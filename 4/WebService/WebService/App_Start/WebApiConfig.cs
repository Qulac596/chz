using System.Web.Http;

namespace WebService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.DependencyResolver = new DependencyResolver();
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Routes.MapHttpRoute(
               name: "DefaultNaklsFinish",
               routeTemplate: "api/Nakls/Finish/{id}",
               defaults: new { controller = "Nakls", action = "Finish", id = RouteParameter.Optional }
               );

            config.Routes.MapHttpRoute(
            name: "NaklControllerTrustAccept",
            routeTemplate: "api/Nakls/TrustAccept/{id}",
            defaults: new { controller = "Nakls", action = "TrustAccept", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
           name: "NaklControllerAdd",
           routeTemplate: "api/Nakls/Add/{id}",
           defaults: new { controller = "Nakls", action = "Add", id = RouteParameter.Optional }
           );

            config.Routes.MapHttpRoute(
            name: "NaklControllerUpdate",
            routeTemplate: "api/Nakls/Update/{id}",
            defaults: new { controller = "Nakls", action = "Update", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
           name: "NaklItemAdd",
           routeTemplate: "api/NaklItems/Add/{id}",
           defaults: new { controller = "NaklItems", action = "Add", id = RouteParameter.Optional }
           );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
