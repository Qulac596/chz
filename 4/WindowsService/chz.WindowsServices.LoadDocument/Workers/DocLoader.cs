using chz.WindowsServices.LoadDocument.DataBase;
using chz.WindowsServices.Log;
using chz.WindowsServices.Workes;
using System;
using System.Collections.Generic;

namespace chz.WindowsServices.LoadDocument.Workers
{
    /*
     * Загружает документ по ссылке
     */
    public class DocLoader : ItemCommandServiceWorker<IncomeDocument>
    {
        private readonly MDLPClientAdapter mDLPClientAdapter;
        private readonly IDataBase dataBase;

        public DocLoader(ILogger log, MDLPClientAdapter mDLPClientAdapter, IDataBase dataBase, ICommandWorker<IncomeDocument> errorManager)
            : base(log, dataBase, errorManager, null, false)
        {
            this.mDLPClientAdapter = mDLPClientAdapter;
            this.dataBase = dataBase;
        }

        protected override string NotExitItemMessage => "DocLoader: Документов нет.";

        protected override string СompletedMessage => "DocLoader: Документ загружен. DocumentId: " + Item.DocumentId;

        protected override int ExecuteRequestMDLPTimeOut => 500;

        protected override void Action()
        {
            Item.IncomeDocumentStatus = ServiceIncomeDocumentStatus.DocLoad;
            Item.CompleteDateTime = DateTime.Now;
        }

        protected override void ExecuteRequestMDLP()
        {
            Item.Content = mDLPClientAdapter.GetDocument(Item.Guid,Item.Link);
        }

        protected override IEnumerable<IncomeDocument> ReadDataBase()
        {
            return dataBase.GetLoadLink();
        }
    }
}
