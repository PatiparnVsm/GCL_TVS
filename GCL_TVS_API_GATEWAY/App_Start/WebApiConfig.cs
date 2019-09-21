using GCL_TVS_API_GATEWAY.Filters;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GCL_TVS_API_GATEWAY
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Filters.Add(new AuthorizeAttribute());

            config.Filters.Add(new ValidateViewModelAttribute());

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Remove the XML formatter (Json Only)
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
