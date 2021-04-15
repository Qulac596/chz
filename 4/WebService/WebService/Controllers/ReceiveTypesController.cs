using System.Collections.Generic;
using System.Web.Http;
using WebService.DataBase.RecesiverType;
using WebService.Models;

namespace WebService.Controllers
{
    public class ReceiveTypesController : ApiController
    {
        private readonly IRecesiverTypeDataBase recesiverTypeDataBase;

        public ReceiveTypesController(IRecesiverTypeDataBase recesiverTypeDataBase)
        {
            this.recesiverTypeDataBase = recesiverTypeDataBase;
        }

        [HttpGet]
        public IEnumerable<ReceiveTypeModel> Get()
        {
            return recesiverTypeDataBase.Get();
        }
    }
}