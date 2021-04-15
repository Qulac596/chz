using chz.WindowsServices.Log;
using chz.WindowsServices.UnloadDocument.DataBase;
using chz.WindowsServices.Workes;
using System.Collections.Generic;

namespace chz.WindowsServices.UnloadDocument.Workers
{
    /*
     * Загружает ссылку на тикет
     */
    public class LinkLoader : ItemCommandServiceWorker<OutcomeDocument>
    {
        protected override string NotExitItemMessage => "LinkLoader: Документов нет.";

        protected override string СompletedMessage => "LinkLoader: Ссылка загружена. DocumentId: " + Item.DocumentId;

        protected override int ExecuteRequestMDLPTimeOut => 500;

        private readonly IDataBase dataBase;
        private readonly MDLPClientAdapter mDLPClientAdapter;

        public LinkLoader(IDataBase dataBase, ILogger log, MDLPClientAdapter mDLPClientAdapter, ICommandWorker<OutcomeDocument> errorManager,
            ICommandWorker<DataBase.OutcomeDocument> ticketLoader) : base(log, dataBase, errorManager, ticketLoader, true)
        {
            this.dataBase = dataBase;
            this.mDLPClientAdapter = mDLPClientAdapter;
        }

        protected override void Action()
        {
            Item.DocumentServiceStatus = DocumentServiceStatus.LoadLink;
        }

        protected override IEnumerable<OutcomeDocument> ReadDataBase()
        {
            return dataBase.GetProccessedDocuments();
        }

        protected override void ExecuteRequestMDLP()
        {
            Item.TicketLink = mDLPClientAdapter.GetDocumentTicketLink(Item.Guid, Item.DocumentId);
        }
    }
}
