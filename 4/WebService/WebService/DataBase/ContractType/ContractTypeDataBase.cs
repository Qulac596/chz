using System.Collections.Generic;
using Tools.SqlTransact;
using WebService.Models;

namespace WebService.DataBase.ContractType
{
    public class ContractTypeDataBase : DataBase, IContractTypeDataBase
    {
        public ContractTypeDataBase(string connectionString) : base(connectionString) { }

        public IEnumerable<ContractTypeModel> Get()
        {
            const string commandText = @"select chz_contract_type_id,name From chz_contract_types";

            var transaction = Transact<ContractTypeModel>.Create(ConnectionString, commandText);
            return transaction.ExecuteReader();
        }
    }
}