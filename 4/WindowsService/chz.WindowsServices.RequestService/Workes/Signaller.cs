using chz.WindowsServices.DataBase;
using chz.WindowsServices.DirectoryLoader.DataBase;
using chz.WindowsServices.Log;
using chz.WindowsServices.Workes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace chz.WindowsServices.DirectoryLoader.Workes
{
    /*
     * Считывает с бад новые запрос к сервису МДЛП
     */
    public class Signaller : ServiceWorker
    {
        private const string NewRequestMessage = "Signaller: Есть новые запросы.";
        private const string NotExitRequestMessage = "Singaller: Запросов нет.";

        private const int MillisecondCount = 1000;

        private readonly IDataBase dataBase;
        private readonly Setting.ISettingProvider setting;
        private readonly ICommandWorker<IEnumerable<Request>> branchLoader;

        private IEnumerable<Request> branchRequests;

        public Signaller(IDataBase dataBase, Setting.ISettingProvider setting, ILogger log, ICommandWorker<IEnumerable<Request>> branchLoader) : base(log)
        {
            this.dataBase = dataBase;
            this.setting = setting;
            this.branchLoader = branchLoader;
        }

        protected override void Execute()
        {
            DateTime = DateTime.Now;

            if (GetBranchRequest() == false)
            {
                return;
            }

            branchRequests = branchRequests.Where(x => x.StartAfte == null || x.StartAfte < DateTime.Now).ToList();
            if (branchRequests.Count() > 0)
            {
                branchLoader.Process(branchRequests);
                Log.WriteInfo(NewRequestMessage);
            }
            else
            {
                Log.WriteInfo(NotExitRequestMessage);
            }

            if (Sleep() == false)
            {
                return;
            }
        }

        /*
         * Считываем новые запросы из бд
         */
        private bool GetBranchRequest()
        {
            bool isError;
            do
            {
                try
                {
                    branchRequests = dataBase.GetNewRequest();
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
         * Выдерживает паузу
         */
        private bool Sleep()
        {
            var timeOut = int.Parse(setting.GetValue("СheckingNewRequestTimeOut")) - (int)(DateTime.Now - DateTime)
                .TotalSeconds;

            for (; timeOut > 0; timeOut--)
            {
                Thread.Sleep(MillisecondCount);
                lock (IsRunLocker)
                {
                    if (IsRun == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}

