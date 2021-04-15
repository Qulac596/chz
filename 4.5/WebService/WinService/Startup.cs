using Microsoft.Owin.StaticFiles;
using Owin;
using System.Web.Http;

namespace WinService
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            WebService.WebApiConfig.Register(config);

            appBuilder.UseFileServer(new FileServerOptions()
            {
                RequestPath = new Microsoft.Owin.PathString(""),
                EnableDefaultFiles = true

            });

            appBuilder.Use<LoggerMiddleware>();

            appBuilder.UseWebApi(config);
        }
    }
}
