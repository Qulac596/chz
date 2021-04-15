using System.Collections.Generic;
using Tools.SqlTransact;
using WebService.Models;

namespace WebService.DataBase.SourceType
{
    public class SourceTypDataBase : DataBase, ISourceTypeDataBase
    {
        public SourceTypDataBase(string connectionString) : base(connectionString) { }

        public IEnumerable<SourceTypeModel> Get()
        {
            const string commandText = @"Select chz_source_type_id,name From chz_source_types";

            var transaction = Transact<SourceTypeModel>.Create(ConnectionString, commandText);
            return transaction.ExecuteReader();
        }
    }
}