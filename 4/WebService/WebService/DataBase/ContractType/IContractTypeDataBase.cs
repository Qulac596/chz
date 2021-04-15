using System.Collections.Generic;
using WebService.Models;

namespace WebService.DataBase.ContractType
{
    public interface IContractTypeDataBase
    {
        IEnumerable<ContractTypeModel> Get();
    }
}