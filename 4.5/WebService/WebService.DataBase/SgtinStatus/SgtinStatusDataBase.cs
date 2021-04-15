using System.Collections.Generic;
using Tools.SqlTransact;

namespace WebService.DataBase.SgtinStatus
{
    public class SgtinStatusDataBase : DataBase
    {
        public SgtinStatusDataBase(string connectionString, string login, string password) : base(connectionString, login, password)
        {

        }

        public IEnumerable<SgtinStatus> GetAll()
        {
            const string commandText = @"Select chz_scaned_sgtin_status_id, value, style 
            From chz_scaned_sgtin_statuses";

            var transaction = Transact<SgtinStatus>.Create(ConnectionString, commandText);
            return transaction.ExecuteReader();
        }
    }
}
