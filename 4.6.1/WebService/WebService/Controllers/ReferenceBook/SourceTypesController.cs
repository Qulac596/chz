using System.Collections.Generic;
using WebService.Filters;
using WebService.Services.ReferenceBook;
using WebService.ViewModel;

namespace WebService.Controllers
{
    [AuthenticationFiltr()]
    public class SourceTypesController : BaseController
    {
        public SourceTypesController(string connectionStringPattern) : base(connectionStringPattern)
        {

        }

        public Result<IEnumerable<SourceTypeViewModel>> Get()
        {
            var sourceTypesService = Create<SourceTypesService>();
            return sourceTypesService.Get();
        }
    }
}