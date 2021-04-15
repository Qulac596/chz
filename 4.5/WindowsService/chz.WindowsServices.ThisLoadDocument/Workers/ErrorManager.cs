using chz.WindowsServices.DataBase;
using chz.WindowsServices.ThisLoadDocument.DataBase;
using chz.WindowsServices.ThisLoadDocument.MDLPClient;
using chz.WindowsServices.Log;
using chz.WindowsServices.Workes;
using Unity;

namespace chz.WindowsServices.ThisLoadDocument.Workers
{
    public class ErrorManager : ErrorManagerBase<DataBase.IncomeDocument, IMDLPClient>
    {
        public ErrorManager(ILogger log, MDLPClientAdapterBase<IMDLPClient> mDLPClientAdapterBase, IDataBase<DataBase.IncomeDocument> dataBase,
            UnityContainer unityContainer) : base(log, mDLPClientAdapterBase, dataBase, unityContainer)
        {
        }

        protected override string NotExitItemMessage => "ErrorManager: Документов нет.";

        protected override string CompletteItemMessage => "ErrorManager: Документ вернут на обработку. DocumentId: " + Item.DocumentId;

        protected override void Transfer()
        {
            switch (Item.IncomeDocumentStatus)
            {
                case ServiceIncomeDocumentStatus.New:
                    UnityContainer.Resolve<ICommandWorker<DataBase.IncomeDocument>>("LinkLoader").Process(Item);
                    break;
                default:
                    UnityContainer.Resolve<ICommandWorker<DataBase.IncomeDocument>>("DocLoader").Process(Item);
                    break;
            }
        }
    }
}
