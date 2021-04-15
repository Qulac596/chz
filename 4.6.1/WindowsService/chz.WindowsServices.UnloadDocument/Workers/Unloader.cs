using chz.WindowsServices.Cryptography;
using chz.WindowsServices.DataBase;
using chz.WindowsServices.Log;
using chz.WindowsServices.UnloadDocument.DataBase;
using chz.WindowsServices.Workes;
using System;
using System.Text;

namespace chz.WindowsServices.UnloadDocument.Workers
{
    /*
     * Отправляет документ на сервис МДЛП
     */
    public class Unloader : ListCommandServiceWorker<OutcomeDocument>
    {
        private readonly MDLPClientAdapter mDLPClientAdapter;
        private readonly ISigner signer;

        public Unloader(IDataBase dataBase, ILogger log, MDLPClientAdapter mDLPClientAdapter, ICommandWorker<OutcomeDocument> errorManager,
            ICommandWorker<OutcomeDocument> statusInspector, ISigner signer) : base(log, dataBase, errorManager, statusInspector, true)
        {
            this.mDLPClientAdapter = mDLPClientAdapter;
            this.signer = signer;
        }

        protected override string NotExitItemMessage => "Unloader: Документов нет.";

        protected override string СompletedMessage => "Unloader: Документ загружен в ЧЗ. DocumentId: " + Item.DocumentId;

        protected override int ExecuteRequestMDLPTimeOut => 500;

        protected override void Action()
        {

            Item.DocumentServiceStatus = DocumentServiceStatus.UnLoad;
        }

        protected override void ExecuteRequestMDLP()
        {
            Item.RequestId = Guid.NewGuid().ToString();
            Item.Base64Content = Convert.ToBase64String(Encoding.UTF8.GetBytes(Item.Content));

            Sign();

            if (Item.Error == null)
            {
                Item.DocumentId = mDLPClientAdapter.SendDocument(Item.Guid, Item.Base64Content, Item.Signature, Item.RequestId);
            }
        }

        private void Sign()
        {
            try
            {
                Item.Signature = signer.Sign(Item.Content);
            }
            catch (CryptographyException ex)
            {
                Log.WriteError(ex);
                Item.SetError(ex.Message, ServiceErrorType.CryptographyError);
            }
        }
    }
}
