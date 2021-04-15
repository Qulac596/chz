using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebService.DataBase.User;

namespace WebService.Filters
{
    public class AuthorizationFiltr : Attribute, IAuthorizationFilter
    {
        private const string connectionString = "Data Source=DESKTOP\\SQLEXPRESS;DataBase=g82; User ID=sa;Password=197312;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private readonly UserDataBase userDataBase = new UserDataBase(connectionString);

        public bool AllowMultiple => false;

        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken,
            Func<Task<HttpResponseMessage>> continuation)
        {
            IEnumerable<string> headers;

            if (actionContext.Request.Headers.TryGetValues("accessToken", out headers))
            {
                var accessToken = headers.First();
                var user = userDataBase.Get(accessToken);
                if (user != null)
                {
                    return continuation();
                }
                else
                {
                    return CreateTask(actionContext);
                }
            }
            else
            {
                return CreateTask(actionContext);
            }
        }

        private Task<HttpResponseMessage> CreateTask(HttpActionContext actionContext)
        {
            var response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            var task = new Task<HttpResponseMessage>(() => response);
            task.Start();
            return task;
        }
    }
}