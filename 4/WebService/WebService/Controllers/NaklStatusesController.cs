using System;
using System.Collections.Generic;
using System.Web.Http;
using WebService.DataBase.NaklStatus;
using WebService.Models;
using System.Net;
using WebService.Filters;

namespace WebService.Controllers
{
    [AuthorizationFiltr()]
    public class NaklStatusesController : ApiController
    {
        private readonly INaklStatusDataBase dataBase;

        public NaklStatusesController(INaklStatusDataBase dataBase)
        {
            this.dataBase = dataBase;
        }

        /*
         * Возвращает статусы накладных для фильтра
         */
        [HttpGet]
        public IEnumerable<NaklStatusModel> Get()
        {
            try
            {
                return dataBase.GetNaklStatusModels();
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}