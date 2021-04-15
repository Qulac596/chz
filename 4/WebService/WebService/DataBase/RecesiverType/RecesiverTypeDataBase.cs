using System.Collections.Generic;
using Tools.SqlTransact;
using WebService.Models;

namespace WebService.DataBase.RecesiverType
{
    public class RecesiverTypeDataBase : DataBase, IRecesiverTypeDataBase
    {
        public RecesiverTypeDataBase(string connectionstring) : base(connectionstring) { }

        public IEnumerable<ReceiveTypeModel> Get()
        {
            const string commandText = @"Select chz_receive_type_id,name
            From chz_receive_types";

            var transaction = Transact<ReceiveTypeModel>.Create(ConnectionString, commandText);
            return transaction.ExecuteReader();
        }
    }
}