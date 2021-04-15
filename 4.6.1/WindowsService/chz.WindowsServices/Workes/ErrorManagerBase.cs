using chz.WindowsServices.DataBase;
using chz.WindowsServices.Log;
using chz.WindowsServices.MDLPClient;
using System;
using System.Collections.Generic;
using System.Threading;
using Unity;

namespace chz.WindowsServices.Workes
{
    public abstract class ErrorManagerBase<TItem, TClient> : ServiceWorker, ICommandWorker<TItem>
        where TItem : DomainObject where TClient : IMDLPClient
    {
        private readonly MDLPClientAdapterBase<TClient> mDLPClientAdapterBase;
        private readonly IDataBase<TItem> dataBase;
        private Queue<TItem> items = new Queue<TItem>();
        private readonly object itemsLocker = new object();
        private readonly AutoResetEvent autoResetEvent = new AutoResetEvent(false);

        public ErrorManagerBase(ILogger log, MDLPClientAdapterBase<TClient> mDLPClientAdapterBase, IDataBase<TItem> dataBase,
            UnityContainer unityContainer) : base(log)
        {
            this.mDLPClientAdapterBase = mDLPClientAdapterBase;
            this.dataBase = dataBase;
            this.UnityContainer = unityContainer;
        }

        protected UnityContainer UnityContainer { get; private set; }

        protected TItem Item { get; private set; }

        public void Process(TItem item)
        {
            lock (itemsLocker)
            {
                items.Enqueue(item);
            }
            autoResetEvent.Set();
        }

        protected override void Execute()
        {
            /*
             * Получаем новый элмент из очереди
             * Если очередь пуста то устанавливаем значение элемента на null
             */
            lock (itemsLocker)
            {
                if (items.Count > 0)
                {
                    Item = items.Dequeue();
                }
                else
                {
                    Item = null;
                }
            }

            if (Item == null)
            {
                /*
                 * Если элементов нет, пишем в лог и встаем в ожидание события
                 */
                Log.WriteInfo(NotExitItemMessage);
                autoResetEvent.WaitOne();
            }
            else
            {
                Action();
            }
        }


        private void Action()
        {
            if (Item.Error is MDLPError)
            {
                mDLPClientAdapterBase.Reboot();
            }
            else
            {
                var error = (ServiceError)Item.Error;

                if (error.ServiceErrorType == ServiceErrorType.HttpRequestError)
                {
                    mDLPClientAdapterBase.Reboot();
                }
            }

            if (Complete() == false)
            {
                return;
            }
        }

        /*
         * Обновляет элементв в бд
         */
        private bool Update()
        {
            bool isError;
            do
            {
                try
                {
                    dataBase.Update(Item);
                    isError = false;
                }
                catch (DataBaseException ex)
                {
                    Log.WriteError(ex);
                    isError = true;
                }

                lock (IsRunLocker)
                {
                    if (IsRun == false)
                    {
                        return false;
                    }
                }
            } while (isError == true);

            return true;
        }

        /*
         * Перед стартом потока заполняем очередь элементами из бд
         */
        protected override void OnStart()
        {
            IEnumerable<TItem> items;
            try
            {
                items = dataBase.GetError();
            }
            catch (DataBaseException e)
            {
                Log.WriteError(e);
                return;
            }

            foreach (var i in items)
            {
                this.items.Enqueue(i);
            }
        }

        private bool Complete()
        {
            Item.Error = null;

            if (Update() == false)
            {
                return false;
            }

            Transfer();

            Log.WriteInfo(CompletteItemMessage);

            return true;
        }

        protected override void OnStop()
        {
            autoResetEvent.Set();
        }

        /*
         * Возвращает элемент источнику на повторную обработку
         */
        protected abstract void Transfer();

        /*
         * Сообщение о отстутсвии элементов
         */
        protected abstract string NotExitItemMessage { get; }

        /*
         * Сообщение о выполнении
         */
        protected abstract string CompletteItemMessage { get; }
    }
}
