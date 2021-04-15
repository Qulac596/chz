using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using WebService.DataBase.ContractType;
using WebService.Models;

namespace WebService.Controllers
{
    public class ContractTypesController : ApiController
    {
        private readonly IContractTypeDataBase contractTypeDataBase;

        public ContractTypesController(IContractTypeDataBase contractTypeDataBase)
        {
            this.contractTypeDataBase = contractTypeDataBase;
        }

        [HttpGet]
        public IEnumerable<ContractTypeModel> Get()
        {
            try
            {
                return contractTypeDataBase.Get();
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}