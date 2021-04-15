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
        public Result<IEnumerable<AddressViewModel>> GetAddress(int id)
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

        [HttpPost]
        [Route("api/companies/add")]
        public Result<int> Add([FromBody] CompanyViewModel companyViewModel)
        {
            var companiesService = Create<CompaniesService>();
            return companiesService.Add(companyViewModel);
        }

        [HttpGet]
        [Route("api/companies/get_by_id/{id}")]
        public Result<CompanyViewModel> GetById(int id)
        {
            var companiesService = Create<CompaniesService>();
            return companiesService.GetById(id);
        }

        [HttpPut]
        [Route("api/companies/update")]
        public Result<object> Update([FromBody] CompanyViewModel companyViewModel)
        {
            var companiesService = Create<CompaniesService>();
            return companiesService.Update(companyViewModel);
        }
    }
}

