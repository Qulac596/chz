using System.Collections.Generic;
using System.Web.Http;
using WebService.Filters;
using WebService.Services;
using WebService.ViewModel;

namespace WebService.Controllers
{
    [AuthenticationFiltr()]
    public class SgtinsController : BaseController
    {
        public SgtinsController(string connectionStringPattern) : base(connectionStringPattern)
        {
        }

        [HttpGet]
        public Result<IEnumerable<SgtinViewModel>> Get(int id)
        {
            var sgtinsService = Create<SgtinsService>();
            return sgtinsService.Get(id);
        }

        [HttpPost]
        public Result<object> Add(int id, string[] sgtins, int? nakl_item_id = null)
        {
            var sgtinsService = Create<SgtinsService>();
            return sgtinsService.Post(id, sgtins, nakl_item_id);
        }
    }
}