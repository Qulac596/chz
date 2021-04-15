using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebService.DataBase.Nakl;
using WebService.DataBase.NaklItem;
using WebService.DataBase.NaklItemStatus;
using WebService.Filters;
using WebService.Services;
using WebService.ViewModel;

namespace WebService.Controllers
{
    [AuthenticationFiltr()]
    public class NaklItemsController : BaseController
    {
        public NaklItemsController(string connectionStringPattern) : base(connectionStringPattern)
        {
        }

        [HttpPut]
        public Result<object> Update(int nakl_item_id, decimal cost, int nds_value_id)
        {
            var sgtinService = Create<NaklItemService>();
            return sgtinService.Update(nakl_item_id, cost, nds_value_id);
        }

        [HttpGet]
        public Result<IEnumerable<NaklItemViewModel>> Get(int id)
        {
            var naklItemService = Create<NaklItemService>();
            return naklItemService.Get(id);
        }
    }
}