using chz.WindowsServices.DataBase;
using chz.WindowsServices.DirectoryLoader.DataBase;
using chz.WindowsServices.DirectoryLoader.MDLPClient;
using chz.WindowsServices.Log;
using chz.WindowsServices.Workes;
using System.Collections.Generic;
using Unity;

namespace chz.WindowsServices.DirectoryLoader.Workes
{
    public class ErrorManager : ErrorManagerBase<Request, IMDLPClient>
    {
        public ErrorManager(ILogger log, MDLPClientAdapterBase<IMDLPClient> mDLPClientAdapterBase, IDataBase<Request> dataBase,
            UnityContainer unityContainer) : base(log, mDLPClientAdapterBase, dataBase, unityContainer)
        {
        }

        protected override string NotExitItemMessage => "ErrorManager: Запросов нет.";

        protected override string CompletteItemMessage => "ErrorManager: Запраос вернут на обработку. BranchId: " + Item.Id;

        protected override void Transfer()
        {
            UnityContainer.Resolve<ICommandWorker<IEnumerable<Request>>>("BranchLoader")
                .Process(new List<Request>() { Item });
        }
    }
}
