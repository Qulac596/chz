using chz.WindowsServices.DataBase;
using chz.WindowsServices.Log;
using chz.WindowsServices.MDLPClient;
using chz.WindowsServices.ThisLoadDocument.DataBase;
using chz.WindowsServices.Workes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;

namespace chz.WindowsServices.ThisLoadDocument.Workers
{
    /*
     * Находит на сервисе МДЛП незагруженные документы и загружает их метаданные
     */
    public class Scanner : ServiceWorker
    {
        private const string NotExitDocumentMessage = "Scaner: Документов нет.";
        private const string LoadMessage = "Scaner: Методанные документа загружены. DocumentId: ";

        private const int MillisecondCount = 1000;
        private const int DocCount = 100;

        private IDataBase dataBase;
        private readonly MDLPClientAdapter mDLPClientAdapter;

        private IncomeDocumentList incomeDocumentList;
        private IEnumerable<IncomeDocument> lastDocuments;
        private IncomeDocument incomeDocument;

        private readonly Setting.ISettingProvider setting;

        private readonly ICommandWorker<IncomeDocument> linkLoader;

        public Scanner(ILogger log, Setting.ISettingProvider settingProvider, MDLPClientAdapter mDLPClientAdapter, IDataBase dataBase, ICommandWorker<IncomeDocument> linkLoader) :
            base(log)
        {
            setting = settingProvider;
            this.dataBase = dataBase;
            this.mDLPClientAdapter = mDLPClientAdapter;
            this.linkLoader = linkLoader;
        }

        protected override void Execute()
        {
            DateTime = DateTime.Now;

            if (GetLastIastDocument() == false)
            {
                return;
            }

            if (lastDocuments.Count() == 0)
            {
                var dateTime = DateTime.Parse(setting.GetValue("StartDateTime"));
                lastDocuments = new List<IncomeDocument>() { new IncomeDocument() { ProcessedDate = dateTime } };
            }

            var index = 0;
            do
            {
                LoadIncomeDocumentList(index);

                if (incomeDocumentList.Error == null)
                {
                    if (incomeDocumentList.Documents.Count() > 0)
                    {
                        foreach (var i in incomeDocumentList.Documents)
                        {
                            if (lastDocuments.Any((x) => x.DocumentId != i.DocumentId))
                            {
                                incomeDocument = i;

                                if (AddDataBase() == false)
                                {
                                    return;
                                }

                                linkLoader.Process(incomeDocument);

                                Log.WriteInfo(LoadMessage + incomeDocument.DocumentId);
                            }
                        }
                    }
                    else
                    {
                        Log.WriteInfo(NotExitDocumentMessage);
                    }

                    index += incomeDocumentList.Documents.Count();
                }
                else
                {
                    if (incomeDocumentList.Error is ServiceError ||
                        ((MDLPError)incomeDocumentList.Error).HttpStatusCode == HttpStatusCode.Unauthorized ||
                       ((MDLPError)incomeDocumentList.Error).HttpStatusCode == HttpStatusCode.Forbidden)
                    {
                        mDLPClientAdapter.Reboot();
                    }

                    continue;
                }

                Thread.Sleep(MillisecondCount);

            } while (incomeDocumentList.Error == null && incomeDocumentList.Documents.Count() > 0);

            if (Sleep() == false)
            {
                return;
            }
        }

        /*
         * Загружает список документов из сервиса МДЛП
         */
        private void LoadIncomeDocumentList(int index)
        {
            try
            {
                incomeDocumentList = mDLPClientAdapter.GetIncomeDocumentList(lastDocuments.First().ProcessedDate.Value, index, DocCount);
            }
            catch (MDLPClientException ex)
            {
                Log.WriteError(ex);
                incomeDocumentList = new IncomeDocumentList();
                incomeDocumentList.SetError(ex.Message, ex.Response, ex.HttpStatusCode);
            }
            catch (Exception e)
            {
                Log.WriteError(e);
                incomeDocumentList.SetError(e.Message, ServiceErrorType.HttpRequestError);
            }
        }

        /*
         * Сохраняет методанные документа в бд
         */
        private bool AddDataBase()
        {
            bool isError;
            do
            {
                try
                {
                    incomeDocument.Id = dataBase.Add(incomeDocument);
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
            var timeOut = int.Parse(setting.GetValue("СheckingDocumentTimeOut")) - (int)(DateTime.Now - DateTime)
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

        private bool GetLastIastDocument()
        {
            bool isError;
            do
            {
                try
                {
                    lastDocuments = dataBase.GetLastDocument();
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
    }
}
