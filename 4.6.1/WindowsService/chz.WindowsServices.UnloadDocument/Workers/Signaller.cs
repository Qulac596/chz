using chz.WindowsServices.DataBase;
using chz.WindowsServices.Log;
using chz.WindowsServices.UnloadDocument.DataBase;
using chz.WindowsServices.Workes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace chz.WindowsServices.UnloadDocument.Workers
{
    /*
     * Считывает с бд новые и отмененные к загрузке документы
     */
    public class Signaller : ServiceWorker
    {
        private const int MillisecondCount = 1000;

        private const string ExitNewDocumentMessage = "Signaller: Есть новые документы.";
        private const string ExitCanceledDocumentMessage = "Signaller: Есть отмененные документы.";
        private const string ExitNewAdnCanceledDocumentMessage = "Signaller: Есть новые и отмененные документы.";
        private const string NoExitDocumentMessage = "Signaller: Документов нет";

        private ICommandWorker<IEnumerable<OutcomeDocument>> canceler, unloader;
        private Signal signal;
        private readonly IDataBase dataBase;
        private readonly Setting.ISettingProvider setting;

        public Signaller(Setting.ISettingProvider setting, IDataBase dataBase, ILogger log, ICommandWorker<IEnumerable<OutcomeDocument>> canceler,
            ICommandWorker<IEnumerable<OutcomeDocument>> unloader)
            : base(log)
        {
            this.setting = setting;
            this.dataBase = dataBase;
            this.canceler = canceler;
            this.unloader = unloader;
        }

        protected override void Execute()
        {
            DateTime = DateTime.Now;

            if (GetSignal() == false)
            {
                return;
            }

            ProcessedSignal();

            if (Sleep() == false)
            {
                return;
            }
        }

        /*
         * Считывает документы из бд
         */
        private bool GetSignal()
        {
            do
            {
                try
                {
                    signal = dataBase.GetSignal();
                }
                catch (DataBaseException ex)
                {
                    Log.WriteError(ex);
                }

                lock (IsRunLocker)
                {
                    if (IsRun == false)
                    {
                        return false;
                    }
                }
            } while (signal == null);

            return true;
        }

        /*
         * Передает документы на обработку следующим workes
         */
        private void ProcessedSignal()
        {
            if (signal.NewDocuments.Count() == 0 && signal.CanceledDocuments.Count() == 0)
            {
                Log.WriteInfo(NoExitDocumentMessage);
            }
            else if (signal.NewDocuments.Count() == 0 && signal.CanceledDocuments.Count() > 0)
            {
                Log.WriteInfo(ExitCanceledDocumentMessage);
                canceler.Process(signal.CanceledDocuments);
            }
            else if (signal.NewDocuments.Count() > 0 && signal.CanceledDocuments.Count() == 0)
            {
                Log.WriteInfo(ExitNewDocumentMessage);
                unloader.Process(signal.NewDocuments);
            }
            else
            {
                Log.WriteInfo(ExitNewAdnCanceledDocumentMessage);
                canceler.Process(signal.CanceledDocuments);
                unloader.Process(signal.NewDocuments);
            }
        }

        /*
         * Выдерживает паузу
         */
        private bool Sleep()
        {
            var timeOut = int.Parse(setting.GetValue("UnloadServiceReadNewTimeOut")) - (int)(DateTime.Now - DateTime)
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
