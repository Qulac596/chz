using chz.WindowsServices.Log;
using chz.WindowsServices.ThisLoadDocument.DataBase;
using chz.WindowsServices.Workes;
using System.Collections.Generic;

namespace chz.WindowsServices.ThisLoadDocument.Workers
{
    /*
     * Загружает ссылку на документ
     */
    public class LinkLoader : ItemCommandServiceWorker<IncomeDocument>
    {
        private readonly MDLPClientAdapter mDLPClientAdapter;
        private readonly IDataBase dataBase;

        public LinkLoader(ILogger log, MDLPClientAdapter mDLPClientAdapter, IDataBase dataBase, ICommandWorker<IncomeDocument> docLoader,
            ICommandWorker<IncomeDocument> errorManager)
            : base(log, dataBase, errorManager, docLoader, true)
        {
            this.mDLPClientAdapter = mDLPClientAdapter;
            this.dataBase = dataBase;
        }

        protected override string NotExitItemMessage => "LinkLoader: Документов нет.";

        protected override string СompletedMessage => "LinkLoader: Ссылка загружена. DocumentId: " + Item.DocumentId;

        protected override int ExecuteRequestMDLPTimeOut => 500;

        protected override void Action()
        {
            Item.IncomeDocumentStatus = ServiceIncomeDocumentStatus.LinkLoad;
        }

        protected override void ExecuteRequestMDLP()
        {
            Item.Link = mDLPClientAdapter.GetDocumentLink(Item.Guid, Item.DocumentId);
        }

        protected override IEnumerable<IncomeDocument> ReadDataBase()
        {
            return dataBase.GetNewIncomeDocument();
        }
    }
}
