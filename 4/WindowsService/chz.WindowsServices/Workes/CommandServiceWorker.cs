using chz.WindowsServices.DataBase;
using chz.WindowsServices.Log;
using chz.WindowsServices.MDLPClient;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace chz.WindowsServices.Workes
{
    /*
     * Активный объект службы работающий с очередью объектов
     */
    public abstract class CommandServiceWorker<TItem> : ServiceWorker where TItem : DomainObject
    {
        /*
         * Менеджер ошибок
         */
        private readonly ICommandWorker<TItem> errorManager;

        /*
         * Текущий элемент
         */
        protected TItem Item { get; private set; }

        /*
         * Дата последнего обращения к сервису МДЛП
         */
        private DateTime? executeRequestMDLPDateTime;

        /*
         * Флаг передачи следующему обработчику
         */
        protected bool CanNext { get; set; }

        protected CommandServiceWorker(ILogger log, IDataBase<TItem> dataBase, ICommandWorker<TItem> errorManager, ICommandWorker<TItem> nextCommandWorker,
            bool canNext) : base(log)
        {
            DataBase = dataBase;
            Queue = new Queue<TItem>();
            AutoResetEvent = new AutoResetEvent(false);
            QueueLocker = new object();
            NextCommandWorker = nextCommandWorker;
            this.errorManager = errorManager;
            CanNext = canNext;
        }

        /*
        * База данных
        */
        protected IDataBase<TItem> DataBase { get; private set; }

        /*
         * Объект блокировки очереди элементов
         */
        protected object QueueLocker { get; private set; }

        /*
         * Очередь элементов
         */
        protected Queue<TItem> Queue { get; private set; }

        /*
         * Приметив синхронизации для передачи события добавления элемента в очередь
         */
        protected AutoResetEvent AutoResetEvent { get; private set; }

        /*
         * Следующий обработчик
         */
        protected ICommandWorker<TItem> NextCommandWorker { get; set; }

        protected override void OnStop()
        {
            AutoResetEvent.Set();
        }

        protected override void Execute()
        {
            /*
             * Получаем новый элмент из очереди
             * Если очередь пуста то устанавливаем значение элемента на null
             */
            lock (QueueLocker)
            {
                if (Queue.Count > 0)
                {
                    Item = Queue.Dequeue();
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
                AutoResetEvent.WaitOne();
            }
            else
            {
                /*
                 * При необходимости выдерживаем тайм-аут
                 */
                if (executeRequestMDLPDateTime != null)
                {
                    Sleep();
                }

                /*
                 * Запоминаем дату обращения к сервису МДЛП
                 */
                executeRequestMDLPDateTime = DateTime.Now;

                /*
                 * Обращаемся к сервису МДЛП
                 */
                try
                {
                    ExecuteRequestMDLP();
                }
                catch (MDLPClientException ex)
                {
                    Log.WriteError(ex);
                    Item.SetError(ex.Message, ex.Response, ex.HttpStatusCode);
                }
                catch (Exception e)
                {
                    Log.WriteError(e);
                    Item.SetError(e.Message, ServiceErrorType.HttpRequestError);
                }


                if (Item.Error == null)
                {
                    /*
                     * Если нет ошибок, то производим обработку
                     */
                    Action();

                    /*
                     * Сохраняем новое состояние элмента в бд
                     */
                    if (Update() == false)
                    {
                        return;
                    }

                    /*
                     * Если есть следующий обработчик и уставлен флаг передачи, то передаем ему на обработку
                     */
                    if (NextCommandWorker != null && CanNext == true)
                    {
                        NextCommandWorker.Process(Item);
                    }

                    /*
                     * Пишем сообщение влог
                     */
                    Log.WriteInfo(СompletedMessage);
                }
                else
                {
                    /*
                     * Сохраняем состояние элмента в бд
                     */
                    if (Update() == false)
                    {
                        return;
                    }

                    if (Item.Error is ServiceError || ((MDLPError)Item.Error).HttpStatusCode == HttpStatusCode.Unauthorized ||
                        ((MDLPError)Item.Error).HttpStatusCode == HttpStatusCode.Forbidden)
                    {
                        /*
                       * Передаем обработчику ошибок
                       */
                        errorManager.Process(Item);
                    }
                    else
                    {
                        /*
                         * Обработка документа закончилась ошибкой
                         */
                        Item.CompleteDateTime = DateTime.Now;

                        /*
                        * Сохраняем состояние элмента в бд
                        */
                        if (Update() == false)
                        {
                            return;
                        }
                    }
                }
            }
        }

        /*
         * Сообщения об отстуствии элементов на обработку
         */
        protected abstract string NotExitItemMessage { get; }

        /*
         * Сообщение об обработке элемента
         */
        protected abstract string СompletedMessage { get; }

        /*
         * Тайм-аут между обращениями к сервису МДЛП
         */
        protected abstract int ExecuteRequestMDLPTimeOut { get; }

        /*
         * Выполняет обращение к сервису МДЛП
         */
        protected abstract void ExecuteRequestMDLP();

        /*
         * Производит обработку элемента
         */
        protected abstract void Action();

        /*
         * Обновляет элементв бд. В случае ошибки циклически повторяет запрос
         */
        private bool Update()
        {
            bool isError;
            do
            {
                try
                {
                    DataBase.Update(Item);
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
         * Выдерживает паузы в работе потока
         */
        private void Sleep()
        {
            var timeOut = (int)(ExecuteRequestMDLPTimeOut - (DateTime.Now - executeRequestMDLPDateTime.Value).TotalMilliseconds);
            if (timeOut > 0)
            {
                Thread.Sleep(timeOut);
            }
        }
    }
}
