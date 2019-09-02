using CIMB.DSE.ML.API.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Mvc;
using System.Web.Routing;

namespace GCL_TVS_API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Add this code, if not present.
            AreaRegistration.RegisterAllAreas();

            //Exception Log
            GlobalConfiguration.Configuration.Services.Add(typeof(IExceptionLogger), new CustomExceptionLog());

            //TracingLog (Request/Response)
            GlobalConfiguration.Configuration.MessageHandlers.Add(new ApiLogHandler());

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
