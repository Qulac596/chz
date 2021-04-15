using System.Collections.Generic;
using Tools.SqlTransact;

namespace WebService.DataBase.SourceType
{
    public class SourceTypeDataBase : DataBase
    {
        public SourceTypeDataBase(string connectionString, string login, string password) : base(connectionString, login, password) { }

        public IEnumerable<SourceType> GetAll()
        {
            const string commandText = @"Select chz_source_type_id,value,is_default 
            From chz_source_types";

            var transaction = Transact<SourceType>.Create(ConnectionString, commandText);
            return transaction.ExecuteReader();
        }
    }
}
