using System.Web.Http;
using WebService.Filters;
using WebService.Services;
using WebService.Services.ReverseAcceptance;
using WebService.ViewModel;

namespace WebService.Controllers
{
    [AuthenticationFiltr()]
    public class NaklFullsController : BaseController
    {
        public NaklFullsController(string connectionStringPattern) : base(connectionStringPattern)
        {
        }

        [HttpGet]
        public Result<NaklFullViewModel> Full(int id)
        {
            var naklService = Create<NaklService>();
            return naklService.Full(id);
        }

        [HttpPost]
        public Result<int> Add([FromBody] NaklFullViewModel naklFullViewModel)
        {
            var reverseAcceptanceNaklService = Create<ReverseAcceptanceNaklService>();
            return reverseAcceptanceNaklService.Add(naklFullViewModel, GetUserId());
        }

        [HttpPut]
        public Result<object> Update([FromBody] NaklFullViewModel naklFullViewModel)
        {
            var naklService = Create<NaklService>();
            return naklService.Update(naklFullViewModel, GetUserId());
        }

        [HttpDelete]
        public Result<object> Delete(int id)
        {
            var reverseAcceptanceNaklService = Create<ReverseAcceptanceNaklService>();
            return reverseAcceptanceNaklService.Delete(id);
        }
    }
}