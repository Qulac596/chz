using System.Collections.Generic;
using Tools.SqlTransact;

namespace WebService.DataBase.AcceptType
{
    public class AcceptTypeDataBase : DataBase
    {
        public AcceptTypeDataBase(string connectionString, string login, string password) : base(connectionString, login, password)
        {

        }

        public IEnumerable<AcceptType> GetAll()
        {
            const string commandText = @"Select chz_nakls__list_of_accept_type_id,value 
            From chz_nakls__list_of_accept_types";

            var transaction = Transact<AcceptType>.Create(ConnectionString, commandText);
            return transaction.ExecuteReader();
        }
    }
}
