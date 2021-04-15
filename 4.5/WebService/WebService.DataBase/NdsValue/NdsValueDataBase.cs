using System.Collections.Generic;
using System.Linq;
using Tools.SqlTransact;

namespace WebService.DataBase.NdsValue
{
    public class NdsValueDataBase : DataBase
    {
        public NdsValueDataBase(string connectionString, string login, string password) : base(connectionString, login, password) { }

        public IEnumerable<NdsValue> GetAll()
        {
            const string commandText = @"Select chz_nds_value_id, value,is_default 
            From chz_nakls__list_of_nds_values";

            var transaction = Transact<NdsValue>.Create(ConnectionString, commandText);
            return transaction.ExecuteReader();
        }

        public NdsValue GetById(int id)
        {
            const string commandText = @"Select chz_nds_value_id, value,is_default
            From chz_nakls__list_of_nds_values
            Where chz_nds_value_id=@chz_nds_value_id";

            var parameter = new NdsIdParameter() { Id = id };
            var transaction = Transact<NdsValue>.Create(ConnectionString, commandText,parameter:parameter);
            return transaction.ExecuteReader().FirstOrDefault();
        }
    }
}
