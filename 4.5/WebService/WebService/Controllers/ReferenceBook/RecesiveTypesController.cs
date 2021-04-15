using System.Collections.Generic;
using System.Web.Http;
using WebService.Filters;
using WebService.Services.ReferenceBook;
using WebService.ViewModel;
namespace WebService.Controllers
{
    [AuthenticationFiltr()]
    public class RecesiveTypesController : BaseController
    {
        public RecesiveTypesController(string connectionStringPattern) : base(connectionStringPattern)
        {

        }

        [HttpGet]
        public Result<IEnumerable<ReceiveTypeViewModel>> Get()
        {
            var recesiveTypesService = Create<RecesiveTypesService>();
            return recesiveTypesService.Get();
        }
    }
}