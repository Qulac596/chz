using chz.WindowsServices.Cryptography;
using chz.WindowsServices.Log;
using chz.WindowsServices.Setting;
using chz.WindowsServices.ThisLoadDocument.MDLPClient;
using System;
using System.Linq;
using System.Threading;
using Unity;

namespace chz.WindowsServices.ThisLoadDocument.Workers
{
    public class MDLPClientAdapter : Workes.MDLPClientAdapterBase<IMDLPClient>
    {

        public MDLPClientAdapter(UnityContainer unityContainer, ILogger log, ISigner signer, ISettingProvider setting) :
            base(unityContainer, log, signer, setting)
        {

        }

        public DataBase.IncomeDocumentList GetIncomeDocumentList(DateTime startDateTime, int startFrom, int count)
        {
            var docFilter = new DocFilter() { ProcessedDateFrom = startDateTime };
            ReaderWriterLockSlim.EnterReadLock();
            try
            {
                var result = Client.GetIncomingDocuments(docFilter, startFrom, count).Result;
                return MapToDomainIncomeDocumentList(result);
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
            finally
            {
                ReaderWriterLockSlim.ExitReadLock();
            }
        }

        public string GetDocumentLink(string targetId, string documentId)
        {
            ReaderWriterLockSlim.EnterReadLock();
            try
            {
                return Client.GetDocumentLink(targetId, documentId).Result.Url;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
            finally
            {
                ReaderWriterLockSlim.ExitReadLock();
            }
        }

        public string GetDocument(string targetId, string url)
        {
            ReaderWriterLockSlim.EnterReadLock();
            try
            {
                return Client.LoadStringContent(url, targetId).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
            finally
            {
                ReaderWriterLockSlim.ExitReadLock();
            }
        }

        private DataBase.IncomeDocumentList MapToDomainIncomeDocumentList(IIncomeDocumentList incomeDocumentList)
        {
            var incomeDocument = new DataBase.IncomeDocumentList();
            incomeDocument.Documents = incomeDocumentList.Documents.Select((x) => MapToDomainIncomeDocument(x));
            incomeDocument.Count = incomeDocumentList.Count;
            return incomeDocument;
        }

        private DataBase.IncomeDocument MapToDomainIncomeDocument(IncomeDocument incomeDocument)
        {
            var doc = new DataBase.IncomeDocument();

            doc.DocumentId = incomeDocument.DocumentId;
            doc.Date = incomeDocument.Date;
            doc.DocType = incomeDocument.DocType;
            doc.ProcessedDate = incomeDocument.ProcessedDate;
            doc.Receiver = incomeDocument.Receiver;
            doc.Sender = incomeDocument.Sender;
            doc.SenderSysId = incomeDocument.SenderSysId;
            doc.Version = incomeDocument.Version;
            doc.Status = incomeDocument.Status;
            doc.SysId = incomeDocument.SysId;
            doc.FileUploadtype = incomeDocument.FileUploadtype;
            doc.RequestId = incomeDocument.RequestId;

            return doc;
        }
    }
}
