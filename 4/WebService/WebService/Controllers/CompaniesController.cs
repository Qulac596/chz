using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using WebService.DataBase.Company;
using WebService.Models;

namespace WebService.Controllers
{
    public class CompaniesController : ApiController
    {
        private readonly ICompanyDataBase companyDataBase;

        public CompaniesController(ICompanyDataBase companyDataBase)
        {
            this.companyDataBase = companyDataBase;
        }

        [HttpGet]
        public IEnumerable<CompanyModel> Get()
        {
            try
            {
                return companyDataBase.Get()
                    .Select((x) => new { company = new { Id = x.companyId, Inn = x.INN, Name = x.Name }, address = new { Id = x.addressId, Text = x.Text } })
                    .GroupBy((x) => x.company).
                    Select((x) => new CompanyModel()
                    {
                        Id = x.Key.Id,
                        Name = x.Key.Name,
                        INN = x.Key.Inn,
                        AddressModels = x.Select((z) => new AddressModel() { Id = z.address.Id, Text = z.address.Text }).ToArray()
                    }).ToList();
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}