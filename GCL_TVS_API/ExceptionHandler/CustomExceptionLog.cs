using CIMB.DSE.ML.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace CIMB.DSE.ML.API.Internal
{
    public class CustomExceptionLog : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            // Trace.TraceError(context.ExceptionContext.Exception.ToString());

            var contextBase = ((HttpContextBase)context.Request.Properties["MS_HttpContext"]);
            var controller = context.Request.GetRouteData().Values["controller"];
            var acction = context.Request.GetRouteData().Values["action"];


            ERROR_LOG_WS data = new ERROR_LOG_WS()
            {
                CLIENT_IP = contextBase.Request.UserHostAddress,
                SERVER_IP = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToString(),
                ERROR_MESSAGE = context.ExceptionContext.Exception.ToString(),
                LOG_LEVEL = "Error",
                SERVICE_NAME = string.Format("{0}/{1}", controller, acction),
                SERVICE_TYPE = "I"
            };

            DSELog.ExceptionLog(data);
        }
    }
}