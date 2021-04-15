using System.Collections.Generic;
using WebService.Filters;
using WebService.Services.ReferenceBook;
using WebService.ViewModel;

namespace WebService.Controllers
{
    [AuthenticationFiltr()]
    public class ContractTypesController : BaseController
    {
        public ContractTypesController(string connectionStringPattern) : base(connectionStringPattern)
        {

        }

        public Result<IEnumerable<ContractTypeViewModel>> Get()
        {
            var contractTypesService = Create<ContractTypesService>();
            return contractTypesService.Get();
        }
    }
}