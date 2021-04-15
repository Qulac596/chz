using chz.WindowsServices.DataBase;
using chz.WindowsServices.Log;
using chz.WindowsServices.UnloadDocument.DataBase;
using chz.WindowsServices.UnloadDocument.MDLPClient;
using chz.WindowsServices.Workes;
using System.Collections.Generic;
using Unity;

namespace chz.WindowsServices.UnloadDocument.Workers
{
    public class ErrorManager : ErrorManagerBase<OutcomeDocument, IMDLPClient>
    {
        protected override string NotExitItemMessage => "ErrorManager: Документов нет.";

        protected override string CompletteItemMessage => "ErrorManager: Документ вернут на обработку. DocumentId: " + Item.DocumentId;

        public ErrorManager(ILogger log, MDLPClientAdapterBase<IMDLPClient> mDLPClientAdapterBase, IDataBase<OutcomeDocument> dataBase,
           UnityContainer unityContainer) : base(log, mDLPClientAdapterBase, dataBase, unityContainer)
        {
        }

        protected override void Transfer()
        {
            if (Item.IsCansel == true)
            {
                UnityContainer.Resolve<ICommandWorker<IEnumerable<OutcomeDocument>>>("Canceler").Process(new List<OutcomeDocument>() { Item });
            }
            else
            {
                switch (Item.DocumentServiceStatus)
                {
                    case DocumentServiceStatus.New:
                        UnityContainer.Resolve<ICommandWorker<IEnumerable<OutcomeDocument>>>("Unloader").Process(new List<OutcomeDocument>() { Item });
                        break;
                    case DocumentServiceStatus.UnLoad:
                        UnityContainer.Resolve<ICommandWorker<OutcomeDocument>>("StatusInspector").Process(Item);
                        break;
                    case DocumentServiceStatus.Processed:
                        UnityContainer.Resolve<ICommandWorker<OutcomeDocument>>("LinkLoader").Process(Item);
                        break;
                    default:
                        UnityContainer.Resolve<ICommandWorker<OutcomeDocument>>("TicketLoader").Process(Item);
                        break;
                }
            }
        }
    }
}
