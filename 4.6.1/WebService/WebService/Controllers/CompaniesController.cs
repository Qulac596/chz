using System.Collections.Generic;
using System.Web.Http;
using WebService.Filters;
using WebService.Services;
using WebService.ViewModel;

namespace WebService.Controllers
{
    [AuthenticationFiltr()]
    public class CompaniesController : BaseController
    {
        public CompaniesController(string connectionStringPattern) : base(connectionStringPattern)
        {
        }

        [HttpGet]
        public Result<IEnumerable<CompanyViewModel>> Get()
        {
            var companiesService = Create<CompaniesService>();
            return companiesService.Get();
        }

        [HttpGet]
        public Result<IEnumerable<AddressViewModel>> Get(int id)
        {
            var companiesService = Create<CompaniesService>();
            return companiesService.Get(id);
        }

        [HttpGet]
        public Result<CompanyResultViewModel> Get(string inn)
        {
            var companiesService = Create<CompaniesService>();
            return companiesService.Get(inn);
        }
    }
}

