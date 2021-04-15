using chz.WindowsServices.Log;
using System;
using System.Threading.Tasks;

namespace chz.WindowsServices.Workes
{
    /*
     * Объект службы работающий в отдельном потоке
     */

    public abstract class ServiceWorker : IWorker
    {
        protected ServiceWorker(ILogger log)
        {
            Log = log;
            IsRunLocker = new object();
        }

        /*
         * Флаг выполнения
         */
        protected bool IsRun { get; set; }

        protected ILogger Log { get; private set; }

        /*
         * Объект для блокировки флага выполения
         */
        protected object IsRunLocker { get; private set; }

        /*
         * Поток выполения
         */
        protected Task Task { get; private set; }

        /*
         * Дата для дочерних классов
         */
        protected DateTime DateTime { get; set; }

        /*
         * Метод в котором происходит выполнение потока
         */
        protected virtual void Run()
        {
            try
            {
                while (true)
                {
                    Execute();

                    lock (IsRunLocker)
                    {
                        if (IsRun == false)
                        {
                            return;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.WriteFatal(e);
            }
        }

        public void Start()
        {
            Task = new Task(Run);
            OnStart();
            IsRun = true;
            Task.Start();
        }

        /*
         * Метод дочернего класса вызываемый циклически в отдельном потоке
         */
        protected abstract void Execute();

        public virtual void Stop()
        {
            lock (IsRunLocker)
            {
                IsRun = false;
            }

            OnStop();

            Task.Wait();
            Task.Dispose();
        }

        /*
         * Метод вызываемый перед стартом потока
         */
        protected virtual void OnStart()
        {
            //пустая реализация
        }

        /*
         * Метод вызываемый перед остановкой потока.
         */
        protected virtual void OnStop()
        {
            //пустая реализация
        }
    }
}
