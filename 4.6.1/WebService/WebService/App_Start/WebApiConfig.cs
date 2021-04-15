using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.DependencyResolver = new DependencyResolver();
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // configure json formatter
            JsonMediaTypeFormatter jsonFormatter = config.Formatters.JsonFormatter;

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            config.Routes.MapHttpRoute(
          name: "Route21",
          routeTemplate: "api/users/company/{id}",
          defaults: new { controller = "Users", action = "GetCompany", id = RouteParameter.Optional }
          );

            config.Routes.MapHttpRoute(
            name: "Route1",
            routeTemplate: "api/users/addresses/{id}",
            defaults: new { controller = "Users", action = "GetAddresses", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
            name: "Route2",
            routeTemplate: "api/companies/addresses/{id}",
            defaults: new { controller = "Companies", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
           name: "Route3",
           routeTemplate: "api/reference-books/source-types/{id}",
           defaults: new { controller = "SourceTypes", id = RouteParameter.Optional }
           );

            config.Routes.MapHttpRoute(
          name: "Route28",
          routeTemplate: "api/reference-books/accept_types/{id}",
          defaults: new { controller = "AcceptTypes", id = RouteParameter.Optional }
          );

            config.Routes.MapHttpRoute(
          name: "Route4",
          routeTemplate: "api/reference-books/receive-types/{id}",
          defaults: new { controller = "RecesiveTypes", id = RouteParameter.Optional }
          );

            config.Routes.MapHttpRoute(
          name: "Route5",
          routeTemplate: "api/reference-books/contract-types/{id}",
          defaults: new { controller = "ContractTypes", id = RouteParameter.Optional }
          );

            config.Routes.MapHttpRoute(
          name: "Route6",
          routeTemplate: "api/reference-books/turnover-types/{id}",
          defaults: new { controller = "TurnoverTypes", id = RouteParameter.Optional }
          );

            config.Routes.MapHttpRoute(
          name: "Route7",
          routeTemplate: "api/reference-books/nakl-statuses/{id}",
          defaults: new { controller = "NaklStatuses", id = RouteParameter.Optional }
          );

            config.Routes.MapHttpRoute(
            name: "Route9",
            routeTemplate: "api/nakl-items/{id}",
            defaults: new { controller = "NaklItems", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
         name: "Route10",
         routeTemplate: "api/reference-books/nds-values/{id}",
         defaults: new { controller = "NdsValues", id = RouteParameter.Optional }
         );

            config.Routes.MapHttpRoute(
         name: "Route14",
         routeTemplate: "api/nakls/filtr/{id}",
         defaults: new { controller = "Nakls", Action = "Filtr", id = RouteParameter.Optional }
         );

            config.Routes.MapHttpRoute(
         name: "Route11",
         routeTemplate: "api/nakls/trust-accept/{id}",
         defaults: new { controller = "Nakls", Action = "TrustAccept", id = RouteParameter.Optional }
         );

            config.Routes.MapHttpRoute(
            name: "Route12",
            routeTemplate: "api/nakls/sign-and-send/{id}",
            defaults: new { controller = "Nakls", Action = "SignAndSend", id = RouteParameter.Optional }
               );


            config.Routes.MapHttpRoute(
               name: "Route8",
               routeTemplate: "api/nakls/{id}",
               defaults: new { controller = "Nakls", Action = "GetNakl", id = RouteParameter.Optional }
               );

            config.Routes.MapHttpRoute(
            name: "Route16",
            routeTemplate: "api/nakls-full/{id}",
            defaults: new { controller = "NaklFulls", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
