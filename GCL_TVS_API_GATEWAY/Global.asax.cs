using CIMB.DSE.ML.API.GATEWAY;
using System.Web.Http;
using System.Web.Mvc;

namespace GCL_TVS_API_GATEWAY
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Add this code, if not present.
            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configuration.MessageHandlers.Add(new ApiLogHandler());

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
