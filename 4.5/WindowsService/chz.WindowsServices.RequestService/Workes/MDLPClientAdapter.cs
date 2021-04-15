using chz.WindowsServices.Cryptography;
using chz.WindowsServices.DirectoryLoader.MDLPClient;
using chz.WindowsServices.Log;
using chz.WindowsServices.Setting;
using System;
using System.Net.Http;
using Unity;

namespace chz.WindowsServices.DirectoryLoader.Workes
{
    public class MDLPClientAdapter : WindowsServices.Workes.MDLPClientAdapterBase<IMDLPClient>
    {
        public MDLPClientAdapter(UnityContainer unityContainer, ILogger log, ISigner signer, ISettingProvider setting)
            : base(unityContainer, log, signer, setting)
        {

        }

        public string GetBranch(string targetId, HttpMethod httpMethod, string url, string json)
        {
            ReaderWriterLockSlim.EnterReadLock();
            try
            {
                return Client.SendRequest(targetId, httpMethod, url, json).Result;
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
