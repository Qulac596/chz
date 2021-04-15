using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebService.DataBase.Nakl;
using WebService.DataBase.NaklItem;
using WebService.DataBase.NaklItemStatus;
using WebService.Filters;
using WebService.ViewModel;

namespace WebService.Controllers
{
    [AuthenticationFiltr()]
    public class NaklItemsController : BaseController
    {
        public NaklItemsController(string connectionStringPattern) : base(connectionStringPattern)
        {
        }

        [HttpGet]
        public Result<IEnumerable<NaklItemViewModel>> Get(int id)
        {
            try
            {
                var naklDataBase = Create<NaklDataBase>();
                var acceptTypeId = naklDataBase.GetAccepTypeId(id);
                if (acceptTypeId == 1)
                {
                    var naklItemStatusDataBase = Create<NaklItemStatusDataBase>();
                    var statuses = naklItemStatusDataBase.GetAll();
                    var naklItemDataBase = Create<NaklItemDataBase>();
                    return new Result<IEnumerable<NaklItemViewModel>>(naklItemDataBase.GetDirectAcceptance(id).Select((x) =>
                   {
                       var n = new NaklItemViewModel()
                       {
                           nakl_item_id = x.Id,
                           name = x.Name,
                           sum = x.Sum,
                           count = x.Count,
                           code_count = x.Count,
                           nds = x.Nds,
                           price = x.Price,
                           validation = "Нет"
                       };

                       if (x.ScenedCount == 0)
                       {
                           n.status = statuses.First((z) => z.Id == 2).Value;
                           n.style = statuses.First((z) => z.Id == 2).Style;
                       }
                       else
                       {
                           if (x.CountMatched == x.Count)
                           {
                               n.status = statuses.First((z) => z.Id == 5).Value;
                               n.style = statuses.First((z) => z.Id == 5).Style;
                           }
                           else
                           {
                               n.status = statuses.First((z) => z.Id == 4).Value;
                               n.style = statuses.First((z) => z.Id == 4).Style;
                           }
                       }
                       return n;
                   }));
                }
                else
                {
                    var naklItemStatusDataBase = Create<NaklItemStatusDataBase>();
                    var statuses = naklItemStatusDataBase.GetAll();
                    var naklItemDataBase = Create<NaklItemDataBase>();
                    return new Result<IEnumerable<NaklItemViewModel>>(naklItemDataBase.GetReverseAcceptance(id).Select((x) =>
                   {
                       var n = new NaklItemViewModel()
                       {
                           nakl_item_id = x.Id,
                           name = x.Name,
                           sum = x.Sum,
                           count = x.Count,
                           code_count = x.Count,
                           nds = x.Nds,
                           price = x.Price,
                           validation = "Нет"
                       };

                       n.status = statuses.First((z) => z.Id == 2).Value;
                       n.style = statuses.First((z) => z.Id == 2).Style;
                       return n;
                   }));
                }
            }
            catch (Exception e)
            {
                return new Result<IEnumerable<NaklItemViewModel>>(e.Message);
            }
        }
    }
}