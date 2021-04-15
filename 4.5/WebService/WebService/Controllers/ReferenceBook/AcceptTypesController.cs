using System.Collections.Generic;
using WebService.Filters;
using WebService.Services.ReferenceBook;
using WebService.ViewModel;

namespace WebService.Controllers
{
    [AuthenticationFiltr()]
    public class AcceptTypesController : BaseController
    {
        public AcceptTypesController(string connectionStringPattern) : base(connectionStringPattern)
        {

        }

        public Result<IEnumerable<AcceptTypeViewModel>> GetAll()
        {
            var acceptTypesService = Create<AcceptTypesService>();
            return acceptTypesService.GetAll();
        }
    }
}