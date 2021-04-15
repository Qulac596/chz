using System.Collections.Generic;
using System.Web.Http;
using WebService.Filters;
using WebService.Services.ReferenceBook;
using WebService.ViewModel;

namespace WebService.Controllers
{
    [AuthenticationFiltr()]
    public class NaklStatusesController : BaseController
    {
        public NaklStatusesController(string connectionStringPattern) : base(connectionStringPattern)
        {

        }

        [HttpGet]
        public Result<IEnumerable<NaklStatusViewModel>> Get()
        {
            var naklStatusesService = Create<NaklStatusesService>();
            return naklStatusesService.Get();
        }
    }
}