using chz.WindowsServices.Log;
using System;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace chz.WindowsServices
{
    /*
     * Базовый класс для служб
     */
    public abstract class MDLPService : ServiceBase
    {
        private const string StartMessage = "Служба запущена.";
        private const string StopMessage = "Служба остановлена.";

        private Task task;

        protected MDLPService() { }

        protected MDLPService(ILogger log)
        {
            Log = log;
            CanPauseAndContinue = false;
            CanShutdown = true;
            AutoLog = true;
        }

        protected ILogger Log { get; set; }

        protected override void OnStart(string[] args)
        {
            Log.WriteInfo(StartMessage);

            task = new TaskFactory().StartNew(() =>
            {
                try
                {
                    StartMDLPService();
                }
                catch (Exception e)
                {
                    Log.WriteFatal(e);
                }
            });
        }

        protected override void OnStop()
        {
            task.Wait();

            try
            {
                StopMDLPService();

                Log.WriteInfo(StopMessage);
            }
            catch (Exception e)
            {
                Log.WriteFatal(e);
            }
        }

        /*
         * Метод для дочерних классов вызывается при старте службы
         */
        protected abstract void StartMDLPService();

        /*
        * Метод для дочерних классов вызывается при остановке службы
        */
        protected abstract void StopMDLPService();
    }
}
