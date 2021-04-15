using System.Collections.Generic;
using Tools.SqlTransact;

namespace WebService.DataBase.NdsValue
{
    public class NdsValueDataBase : DataBase
    {
        public NdsValueDataBase(string connectionString, string login, string password) : base(connectionString, login, password) { }

        public IEnumerable<NdsValue> GetAll()
        {
            const string commandText = @"Select value,is_default 
            From chz_nakls__list_of_nds_values";

            var transaction = Transact<NdsValue>.Create(ConnectionString, commandText);
            return transaction.ExecuteReader();
        }
    }
}
