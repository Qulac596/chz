using System;
using System.Collections.Generic;
using System.Linq;
using WebService.DataBase.ContractType;
using WebService.ViewModel;

namespace WebService.Services.ReferenceBook
{
    public class ContractTypesService : ServiceBase
    {
        public ContractTypesService(string connectionStringPattern, string login, string password) : base(connectionStringPattern, login, password) { }

        public Result<IEnumerable<ContractTypeViewModel>> Get()
        {
            try
            {
                var contractTypeDataBase = new ContractTypeDataBase(ConnectionStringPattern, Login, Password);

                return new Result<IEnumerable<ContractTypeViewModel>>(contractTypeDataBase.GetAll()
                .Select((x) => new ContractTypeViewModel() { contract_type_id = x.Id, value = x.Value, is_default = x.IsDefault })
                .ToList());
            }
            catch (Exception e)
            {
                return new Result<IEnumerable<ContractTypeViewModel>>(e.Message);
            }
        }
    }
}
