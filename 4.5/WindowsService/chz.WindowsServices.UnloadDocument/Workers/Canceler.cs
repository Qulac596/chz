using chz.WindowsServices.Log;
using chz.WindowsServices.UnloadDocument.DataBase;
using chz.WindowsServices.Workes;
using System;

namespace chz.WindowsServices.UnloadDocument.Workers
{
    /*
     * Отменяет отправку документа
     */
    public class Canceler : ListCommandServiceWorker<OutcomeDocument>
    {
        private readonly MDLPClientAdapter mDLPClientAdapter;

        public Canceler(IDataBase dataBase, ILogger log, MDLPClientAdapter mDLPClientAdapter, ICommandWorker<OutcomeDocument> errorManager)
            : base(log, dataBase, errorManager, null, false)
        {
            this.mDLPClientAdapter = mDLPClientAdapter;
        }

        protected override string NotExitItemMessage => "Canceler: Документов нет.";

        protected override string СompletedMessage => "Canceler: Загрузка документа оменена. DocumentId: " + Item.DocumentId;

        protected override int ExecuteRequestMDLPTimeOut => 500;

        protected override void Action()
        {
            Item.Cleaning();
            Item.CompleteDateTime = DateTime.Now;
        }

        protected override void ExecuteRequestMDLP()
        {
            mDLPClientAdapter.CancelSendDocument(Item.Guid, Item.DocumentId, Item.RequestId);
        }
    }
}
