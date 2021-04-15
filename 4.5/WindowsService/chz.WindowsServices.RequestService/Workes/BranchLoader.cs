using chz.WindowsServices.DirectoryLoader.DataBase;
using chz.WindowsServices.Log;
using chz.WindowsServices.Workes;
using System;

namespace chz.WindowsServices.DirectoryLoader.Workes
{
    /*
     * Получает результат запроса из сервера МДЛП
     */
    public class BranchLoader : ListCommandServiceWorker<Request>
    {
        private readonly MDLPClientAdapter mDLPClientAdapter;

        protected override string NotExitItemMessage => "BranchLoader: Запросов нет.";

        protected override string СompletedMessage => "BranchLoader: Запрос выполнен BranchId: " + Item.Id;

        protected override int ExecuteRequestMDLPTimeOut => 1000;


        public BranchLoader(ILogger log, IDataBase dataBase, MDLPClientAdapter mDLPClientAdapter, ICommandWorker<Request> errorManager)
            : base(log, dataBase, errorManager, null, false)
        {
            this.mDLPClientAdapter = mDLPClientAdapter;
        }

        protected override void Action()
        {
            Item.ServiceStatus = ServiceStatus.Load;
            Item.CompleteDateTime = DateTime.Now;
        }


        protected override void ExecuteRequestMDLP()
        {
            Item.ResponseJson = mDLPClientAdapter.GetBranch(Item.Guid, Item.Method, Item.Url, Item.RequestJson);
        }
    }
}
