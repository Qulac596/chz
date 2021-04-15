using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using WebService.DataBase.Nakl;
using WebService.DataBase.NaklItem;
using WebService.Models;

namespace WebService.Controllers
{
    public class NaklsController : ApiController
    {
        private readonly INaklDataBase naklDataBase;
        private readonly INaklItemDataBase naklItemDataBase;

        public NaklsController(INaklDataBase naklDataBase, INaklItemDataBase naklItemDataBase)
        {
            this.naklDataBase = naklDataBase;
            this.naklItemDataBase = naklItemDataBase;
        }

        [HttpGet]
        public NaklModel Get(int naklId)
        {
            try
            {
                return naklDataBase.Get(naklId);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        public IEnumerable<NaklGridModel> Get(int year, int month, int addressId)
        {
            var startDateTime = new DateTime(year, month, 1);
            var endDateTime = startDateTime.AddMonths(1);
            try
            {
                return naklDataBase.Get(startDateTime, endDateTime, addressId);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        public IEnumerable<NaklGridModel> Get(int year, int month, int statusId, int addressId)
        {
            var startDateTime = new DateTime(year, month, 1);
            var endDateTime = startDateTime.AddMonths(1);
            try
            {
                return naklDataBase.Get(startDateTime, endDateTime, statusId, addressId);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        public void Finish(int naklId, DateTime dateTime)
        {
            try
            {
                naklDataBase.Finish(naklId, dateTime);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        public void TrustAccept(int naklId, DateTime dateTime)
        {
            try
            {
                naklDataBase.TrustAccept(naklId, dateTime);

                //устанавливаем для всех gtins накладной статус "Проверили, данные совпадают"
                naklItemDataBase.SetAllStatus(naklId, 3);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public int Add([FromBody] CreateNaklModel createNaklModel)
        {
            try
            {
                naklDataBase.Add(createNaklModel);
                return naklDataBase.GetMaxId();
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        public void Update([FromBody] CreateNaklModel createNaklModel)
        {
            naklDataBase.SaveChanges(createNaklModel);
        }

        [HttpDelete]
        public void Delete(int naklId)
        {
            naklDataBase.Delete(naklId);
        }
    }
}