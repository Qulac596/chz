using System.Collections.Generic;
using System.Web.Http;
using WebService.Filters;
using WebService.Services;
using WebService.Services.DirectAcceptance;
using WebService.ViewModel;

namespace WebService.Controllers
{
    [AuthenticationFiltr()]
    public class NaklsController : BaseController
    {
        public NaklsController(string connectionStringPattern) : base(connectionStringPattern)
        {

        }

        [HttpGet]
        public Result<IEnumerable<NaklGridViewModel>> Filtr(int? company_id = null, int? year = null, int? month = null, int? status_id = null)
        {
            var naklService = Create<NaklService>();
            return naklService.Filtr(company_id, year, month, status_id);
        }


        [HttpGet]
        public Result<NaklViewModel> GetNakl(int id)
        {
            var naklService = Create<NaklService>();
            return naklService.GetNakl(id);
        }

        [HttpPut]
        public Result<object> TrustAccept(int id)
        {
            var directAcceptanceNaklService = Create<DirectAcceptanceNaklService>();
            return directAcceptanceNaklService.TrustAccept(id);
        }

        [HttpPut]
        public Result<object> SignAndSend(int id)
        {
            var naklService = Create<NaklService>();
            return naklService.SignAndSend(id);
        }
    }
}