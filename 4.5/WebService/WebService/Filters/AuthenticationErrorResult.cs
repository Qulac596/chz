using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using WebService.ViewModel;

namespace WebService.Filters
{
    public class AuthenticationErrorResult : IHttpActionResult
    {
        private readonly string message;

        public AuthenticationErrorResult(string message)
        {
            this.message = message;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            responseMessage.StatusCode = System.Net.HttpStatusCode.OK;
            var result = new Result<object>(message);
            var formatter = new JsonMediaTypeFormatter();
            responseMessage.Content = new ObjectContent<Result<object>>(result, formatter, "application/json");

            return responseMessage;
        }
    }
}