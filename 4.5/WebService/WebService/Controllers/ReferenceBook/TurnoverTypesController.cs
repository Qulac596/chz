using System.Collections.Generic;
using System.Web.Http;
using WebService.Filters;
using WebService.Services.ReferenceBook;
using WebService.ViewModel;

namespace WebService.Controllers
{
    [AuthenticationFiltr()]
    public class TurnoverTypesController : BaseController
    {
        public TurnoverTypesController(string connectionStringPattern) : base(connectionStringPattern)
        {
        }

        [HttpGet]
        public Result<IEnumerable<TurnoverTypeViewModel>> Get()
        {
            var turnoverTypesService = Create<TurnoverTypesService>();
            return turnoverTypesService.Get();
        }
    }
}