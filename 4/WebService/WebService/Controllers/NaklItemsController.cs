using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using WebService.DataBase.NaklItem;
using WebService.Models;

namespace WebService.Controllers
{
    public class NaklItemsController : ApiController
    {
        private readonly INaklItemDataBase naklItemDataBase;

        public NaklItemsController(INaklItemDataBase naklItemDataBase)
        {
            this.naklItemDataBase = naklItemDataBase;
        }

        [HttpGet]
        public IEnumerable<NaklItemModel> Get(int naklId)
        {
            try
            {
                return naklItemDataBase.Get(naklId);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public int Add([FromBody] CreateNaklItemModel createNaklItemModel)
        {
            try
            {
                naklItemDataBase.Add(createNaklItemModel);
                return naklItemDataBase.GetMaxId();
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        public void Update([FromBody] CreateNaklItemModel createNaklItemModel)
        {
            try
            {
                naklItemDataBase.SaveChanges(createNaklItemModel);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        public void Delete(int naklItemId)
        {
            naklItemDataBase.Delete(naklItemId);
        }
    }
}