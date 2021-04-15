using chz.WindowsServices.DataBase;
using chz.WindowsServices.Log;
using System.Collections.Generic;

namespace chz.WindowsServices.Workes
{
    /*
     * Принимает на обработку список элементов
     */
    public abstract class ListCommandServiceWorker<TItem> : CommandServiceWorker<TItem>, ICommandWorker<IEnumerable<TItem>> where TItem : DomainObject
    {
        protected ListCommandServiceWorker(ILogger log, IDataBase<TItem> dataBase, ICommandWorker<TItem> errorManager, ICommandWorker<TItem> nextCommandWorker, bool canNext)
            : base(log, dataBase, errorManager, nextCommandWorker, canNext)
        {
        }

        /*
         * Добавляет элементы в очередь
         */
        public void Process(IEnumerable<TItem> item)
        {
            lock (QueueLocker)
            {
                foreach (var i in item)
                {
                    Queue.Enqueue(i);
                }
            }

            AutoResetEvent.Set();
        }
    }
}
