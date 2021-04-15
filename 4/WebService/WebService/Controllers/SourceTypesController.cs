using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using WebService.DataBase.SourceType;
using WebService.Models;

namespace WebService.Controllers
{
    public class SourceTypesController : ApiController
    {
        private ISourceTypeDataBase sourceTypeDataBase;

        public SourceTypesController(ISourceTypeDataBase sourceTypeDataBase)
        {
            this.sourceTypeDataBase = sourceTypeDataBase;
        }

        public IEnumerable<SourceTypeModel> Get()
        {
            try
            {
                return sourceTypeDataBase.Get();
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}