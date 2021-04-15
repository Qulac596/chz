using System.Collections.Generic;
using Tools.SqlTransact;

namespace WebService.DataBase.RecesiveType
{
    public class RecesiveTypeDataBase : DataBase
    {
        public RecesiveTypeDataBase(string connectionString, string login, string password) : base(connectionString, login, password) { }

        public IEnumerable<RecesiveType> GetAll()
        {
            const string commandText = @"Select chz_receive_type_id,value,is_default 
            From chz_receive_types";

            var transaction = Transact<RecesiveType>.Create(ConnectionString, commandText);
            return transaction.ExecuteReader();
        }
    }
}
