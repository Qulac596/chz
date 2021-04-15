using chz.Common;
using chz.WindowsServices.Log;
using chz.WindowsServices.UnloadDocument.DataBase;
using chz.WindowsServices.Workes;
using System.Collections.Generic;

namespace chz.WindowsServices.UnloadDocument.Workers
{
    /*
     * Проверяет статус отправленного документа
     */
    public class StatusInspector : ItemCommandServiceWorker<OutcomeDocument>
    {
        protected override string NotExitItemMessage => "StatusInspector: Документов нет.";

        protected override string СompletedMessage => "StatusInspector: Статус документа проверен. DocumentId: " + Item.DocumentId;

        protected override int ExecuteRequestMDLPTimeOut => 500;

        private readonly IDataBase dataBase;
        private readonly MDLPClientAdapter mDLPClientAdapter;

        public StatusInspector(IDataBase dataBase, ILogger log, MDLPClientAdapter mDLPClientAdapter, ICommandWorker<DataBase.OutcomeDocument> errorManager,
            ICommandWorker<DataBase.OutcomeDocument> linkLoader) : base(log, dataBase, errorManager, linkLoader, true)
        {
            this.dataBase = dataBase;
            this.mDLPClientAdapter = mDLPClientAdapter;
        }

        protected override void Action()
        {
            Item.AttemptCount++;

            if (Item.MDLPDocumentStatus == MDLPDocumentStatus.PROCESSED_DOCUMENT ||
                Item.MDLPDocumentStatus == MDLPDocumentStatus.FAILED_RESULT_READY)
            {
                Item.DocumentServiceStatus = DocumentServiceStatus.Processed;

                CanNext = true;
            }
            else
            {
                CanNext = false;
                base.Queue.Enqueue(Item);
            }
        }

        protected override IEnumerable<OutcomeDocument> ReadDataBase()
        {
            return dataBase.GetUnloadDocuments();
        }

        protected override void ExecuteRequestMDLP()
        {
            Item.MDLPDocumentStatus = mDLPClientAdapter.GetDocumentStatus(Item.Guid,Item.DocumentId);
        }
    }
}
