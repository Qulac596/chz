using chz.WindowsServices.DataBase;
using chz.WindowsServices.Log;
using System.Collections.Generic;

namespace chz.WindowsServices.Workes
{
    /*
     * Принимает на обработку элементы по одному
     */
    public abstract class ItemCommandServiceWorker<TItem> : CommandServiceWorker<TItem>, ICommandWorker<TItem> where TItem : DomainObject
    {
        protected ItemCommandServiceWorker(ILogger log, IDataBase<TItem> dataBase, ICommandWorker<TItem> errorManager, ICommandWorker<TItem> nextCommandWorker, bool canNext)
            : base(log, dataBase, errorManager, nextCommandWorker, canNext)
        {
        }

        /*
         * Добавляет элемент в очередь на обработку
         */
        public void Process(TItem item)
        {
            lock (QueueLocker)
            {
                Queue.Enqueue(item);
            }

            AutoResetEvent.Set();
        }

        /*
         * Перед стартом потока заполняем очередь элементами из бд
         */
        protected override void OnStart()
        {
            IEnumerable<TItem> items;
            try
            {
                items = ReadDataBase();
            }
            catch (DataBaseException e)
            {
                Log.WriteError(e);
                return;
            }

            foreach (var i in items)
            {
                Queue.Enqueue(i);
            }
        }

        /*
         * Считывает элементы из бд при старте
         */
        protected abstract IEnumerable<TItem> ReadDataBase();
    }
}
