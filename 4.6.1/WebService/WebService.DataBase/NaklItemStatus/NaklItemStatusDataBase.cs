using System.Collections.Generic;
using Tools.SqlTransact;

namespace WebService.DataBase.NaklItemStatus
{
    public class NaklItemStatusDataBase : DataBase
    {
        public NaklItemStatusDataBase(string connectionString, string login, string password) : base(connectionString, login, password)
        {

        }

        public IEnumerable<NaklItemStatus> GetAll()
        {
            const string commandText = @"Select chz_nakl_item_status_id,value,style From chz_nakl_item_statuses";

            var transaction = Transact<NaklItemStatus>.Create(ConnectionString, commandText);
            return transaction.ExecuteReader();
        }
    }
}
