using System.Collections.Generic;
using Tools.SqlTransact;

namespace WebService.DataBase.ContractType
{
    public class ContractTypeDataBase : DataBase
    {
        public ContractTypeDataBase(string connectionString, string login, string password) : base(connectionString, login, password) { }

        public IEnumerable<ContractType> GetAll()
        {
            const string commandText = @"Select chz_contract_type_id,value,is_default 
            From chz_contract_types";

            var transaction = Transact<ContractType>.Create(ConnectionString, commandText);
            return transaction.ExecuteReader();
        }
    }
}
