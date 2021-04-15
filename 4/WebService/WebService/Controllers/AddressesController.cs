using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using WebService.DataBase.Address;
using WebService.Filters;
using WebService.Models;

namespace WebService.Controllers
{
    [AuthorizationFiltr()]
    public class AddressesController : ApiController
    {
        private readonly IAddressDataBase addressDataBase;

        public AddressesController(IAddressDataBase addressDataBase)
        {
            this.addressDataBase = addressDataBase;
        }

        /*
         * Вовзращает адреса огранизации к которой относится пользователь
         */
        [HttpGet]
        public IEnumerable<AddressModel> Get()
        {
            var accessToken = Request.Headers.GetValues("accessToken").First();
            try
            {
               return addressDataBase.Get(accessToken);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}