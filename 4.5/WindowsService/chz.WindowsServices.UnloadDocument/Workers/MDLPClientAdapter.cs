using chz.Common;
using chz.WindowsServices.Cryptography;
using chz.WindowsServices.Log;
using chz.WindowsServices.Setting;
using chz.WindowsServices.Workes;
using System;
using System.Threading;
using Unity;

namespace chz.WindowsServices.UnloadDocument.Workers
{
    public class MDLPClientAdapter : MDLPClientAdapterBase<MDLPClient.IMDLPClient>
    {
        public MDLPClientAdapter(UnityContainer unityContainer, ILogger log, ISigner signer, ISettingProvider setting)
            : base(unityContainer, log, signer, setting)
        {

        }

        public void CancelSendDocument(string targetId, string document_id, string request_id)
        {
            ReaderWriterLockSlim.EnterReadLock();
            try
            {
                Client.CancelSendDocument(targetId, document_id, request_id).Wait();
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

        public MDLPDocumentStatus? GetDocumentStatus(string targetId, string documentId)
        {
            ReaderWriterLockSlim.EnterReadLock();
            try
            {
                return Client.GetOutcomeDocumentMetadata(targetId, documentId).Result.Status;
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

        public string GetDocumentTicketLink(string targetId, string documentId)
        {
            ReaderWriterLockSlim.EnterReadLock();
            try
            {
                return Client.GetDocumentTicketLink(targetId, documentId).Result.Url;
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

        public string GetTicket(string targetId, string url)
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

        public string SendDocument(string targetId, string document, string sign, string requestId)
        {
            ReaderWriterLockSlim.EnterReadLock();
            try
            {
                return Client.SendDocument(targetId, document, sign, requestId).Result.Id;
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
    }
}
