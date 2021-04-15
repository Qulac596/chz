using Microsoft.Owin;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebService.MemoryDataBase;
using WinService.Logger;
using System.Text.RegularExpressions;

namespace WinService
{
    public class LoggerMiddleware : OwinMiddleware
    {
        public LoggerMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            var request = Create(context.Request);

            var body = context.Response.Body;

            var memoryStream = new MemoryStream();

            context.Response.Body = memoryStream;

            await Next.Invoke(context);

            var response = new HttpResponse()
            {
                StatusCode = context.Response.StatusCode
            };

            memoryStream.Position = 0;
            var streamReader = new StreamReader(memoryStream);
            var resposeResult = streamReader.ReadToEnd();
            response.Result = Regex.Replace(resposeResult, "\"access_token\":\".{36}\"", "\"access_token\":\"******************\"");

            memoryStream.Position = 0;
            memoryStream.CopyTo(body);

            context.Response.Body = body;

            streamReader.Dispose();
            memoryStream.Dispose();

            HttpLogger.Write(request, response);
        }

        private HttpRequest Create(IOwinRequest owinRequest)
        {
            var request = new HttpRequest()
            {
                DateTime = DateTime.Now,
                Host = owinRequest.Host.Value,
                Method = owinRequest.Method,
                Url = owinRequest.Path.Value,
                QueryString = owinRequest.QueryString.Value
            };

            string[] headers;
            if (owinRequest.Headers.TryGetValue("accessToken", out headers) == true)
            {
                var accessToken = headers.First();
                var user = UsersStorage.Find(accessToken, DateTime.Now);
                if (user != null)
                {
                    request.Login = user.Login;
                }
            }

            using (var reader = new StreamReader(owinRequest.Body, Encoding.UTF8))
            {
                request.Body = reader.ReadToEnd();
            }

            var memoryStream = new MemoryStream();
            var bytesToWrite = Encoding.UTF8.GetBytes(request.Body);
            memoryStream.Write(bytesToWrite, 0, bytesToWrite.Length);
            memoryStream.Seek(0, SeekOrigin.Begin);

            owinRequest.Body = memoryStream;

            return request;
        }
    }
}
