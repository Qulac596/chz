using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using WebService.DataBase.Nakl;
using WebService.DataBase.NaklItem;
using WebService.DataBase.Sgtin;
using WebService.Filters;
using WebService.Models;

namespace WebService.Controllers
{
    [AuthorizationFiltr()]
    public class SgtinsController : ApiController
    {
        private readonly ISgtinDataBase sgtinDataBase;
        private readonly INaklDataBase naklDataBase;
        private readonly INaklItemDataBase naklItemDataBase;

        public SgtinsController(ISgtinDataBase dataBase, INaklDataBase naklDataBase, INaklItemDataBase naklItemDataBase)
        {
            this.sgtinDataBase = dataBase;
            this.naklDataBase = naklDataBase;
            this.naklItemDataBase = naklItemDataBase;
        }

        public IEnumerable<SgtinModel> Get(int gtinId)
        {
            try
            {
                return sgtinDataBase.Get(gtinId);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public void AddScannedSgtins(int gtinId, [FromBody] string[] sgtins)
        {
            try
            {
                sgtinDataBase.AddScannedSgtins(gtinId, sgtins);
                var is_direct = naklDataBase.GetIsDirect(gtinId);

                //если прямой акцепт, то проводим проверку совпадения данных
                if (is_direct == true)
                {
                    var validateResult = sgtinDataBase.MatchCheck(gtinId);

                    if (validateResult == true)
                    {
                        naklItemDataBase.SetOneStatus(gtinId, 3);
                    }
                    else
                    {
                        naklItemDataBase.SetOneStatus(gtinId, 2);
                    }
                }
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        public void Reset(int gtinId)
        {
            try
            {
                sgtinDataBase.Reset(gtinId);
                naklItemDataBase.SetAllStatus(gtinId, 1);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}