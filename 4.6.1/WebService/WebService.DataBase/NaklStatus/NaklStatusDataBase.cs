using System.Collections.Generic;
using Tools.SqlTransact;

namespace WebService.DataBase.NaklStatus
{
    public class NaklStatusDataBase : DataBase
    {
        public NaklStatusDataBase(string connectionString, string login, string password) : base(connectionString, login, password) { }

        public IEnumerable<NaklStatus> GetAll()
        {
            const string commandText = @"Select chz_nakl_status_id,value,class_name 
            From chz_nakl_statuses";

            var transaction = Transact<NaklStatus>.Create(ConnectionString, commandText);
            return transaction.ExecuteReader();
        }
    }
}
