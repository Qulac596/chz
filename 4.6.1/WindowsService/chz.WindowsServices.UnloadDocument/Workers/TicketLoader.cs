using chz.WindowsServices.Log;
using chz.WindowsServices.UnloadDocument.DataBase;
using chz.WindowsServices.Workes;
using System;
using System.Collections.Generic;
using System.Collections.Generic;

namespace chz.WindowsServices.UnloadDocument.Workers
{
    /*
     * Загружает тикет
     */
    public class TicketLoader : ItemCommandServiceWorker<OutcomeDocument>
    {
        protected override string NotExitItemMessage => "TicketLoader: Документов нет.";

        protected override string СompletedMessage => "TicketLoader: Тикет загружен. DocumentId: " + Item.DocumentId;

        protected override int ExecuteRequestMDLPTimeOut => 500;

        private readonly IDataBase dataBase;
        private MDLPClientAdapter mDLPClientAdapter;

        public TicketLoader(IDataBase dataBase, ILogger log, MDLPClientAdapter mDLPClientAdapter, 
            ICommandWorker<DataBase.OutcomeDocument> errorManager)
            : base(log, dataBase, errorManager, null, false)
        {
            this.dataBase = dataBase;
            this.mDLPClientAdapter = mDLPClientAdapter;
        }

        protected override void Action()
        {
            Item.DocumentServiceStatus = DocumentServiceStatus.LoadTicket;
            Item.CompleteDateTime = DateTime.Now;
        }

        protected override IEnumerable<OutcomeDocument> ReadDataBase()
        {
            return dataBase.GetLinkLoadDocument();
        }

        protected override void ExecuteRequestMDLP()
        {
            Item.Ticket = mDLPClientAdapter.GetTicket(Item.Guid, Item.TicketLink);
        }
    }
}
