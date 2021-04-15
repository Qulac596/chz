using System.Collections.Generic;
using WebService.Filters;
using WebService.Services.ReferenceBook;
using WebService.ViewModel;

namespace WebService.Controllers
{
    [AuthenticationFiltr()]
    public class NdsValuesController : BaseController
    {
        public NdsValuesController(string connectionStringPattern) : base(connectionStringPattern)
        {

        }

        public Result<IEnumerable<NdsValueViewModel>> Get()
        {
            var ndsValuesService = Create<NdsValuesService>();
            return ndsValuesService.Get();
        }
    }
}