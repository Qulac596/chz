using System.Collections.Generic;
using Tools.SqlTransact;

namespace WebService.DataBase.TurnoverType
{
    public class TurnverTypeDataBase : DataBase
    {
        public TurnverTypeDataBase(string connectionString, string login, string password) : base(connectionString, login, password) { }

        public IEnumerable<TurnoverType> GetAll()
        {
            const string commandText = @"Select chz_turnover_tupe_id,value,is_default 
            From chz_turnover_types";

            var transaction = Transact<TurnoverType>.Create(ConnectionString, commandText);
            return transaction.ExecuteReader();
        }
    }
}
