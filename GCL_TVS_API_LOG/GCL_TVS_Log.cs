using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIMB.DSE.ML.Logs
{
    public class DSELog
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static string ChannelType = "";
        public static void TracingLog(TRACE_LOG_WEB_API data)
        {
            try
            {
                logger = LogManager.GetLogger("LogReqestResponse");

                NLog.MappedDiagnosticsContext.Set("Application", data.Application);
                NLog.MappedDiagnosticsContext.Set("ControllerName", data.ControllerName);
                NLog.MappedDiagnosticsContext.Set("ActionName", data.ActionName);
                //NLog.MappedDiagnosticsContext.Set("UserID", data.UserID);
                NLog.MappedDiagnosticsContext.Set("Machine", data.Machine);
                NLog.MappedDiagnosticsContext.Set("MachineIpAddress", data.MachineIpAddress);
                NLog.MappedDiagnosticsContext.Set("RequestIpAddress", data.RequestIpAddress);
                NLog.MappedDiagnosticsContext.Set("RequestTimestamp", data.RequestTimestamp);
                NLog.MappedDiagnosticsContext.Set("ResponseTimestamp", data.ResponseTimestamp);
                NLog.MappedDiagnosticsContext.Set("RequestHeaders", data.RequestHeaders);
                NLog.MappedDiagnosticsContext.Set("RequestUri", data.RequestUri);
                NLog.MappedDiagnosticsContext.Set("ApiPath", data.ApiPath);
                NLog.MappedDiagnosticsContext.Set("RequestContentType", data.RequestContentType);
                NLog.MappedDiagnosticsContext.Set("RequestMethod", data.RequestMethod);
                NLog.MappedDiagnosticsContext.Set("RequestURLParams", data.RequestURLParams);
                NLog.MappedDiagnosticsContext.Set("RequestContentBody", data.RequestContentBody);
                NLog.MappedDiagnosticsContext.Set("ResponseContentType", data.ResponseContentType);
                NLog.MappedDiagnosticsContext.Set("ResponseContentBody", data.ResponseContentBody);
                NLog.MappedDiagnosticsContext.Set("ResponseStatusCode", data.ResponseStatusCode);
                NLog.MappedDiagnosticsContext.Set("ResponseHeaders", data.ResponseHeaders);
                NLog.MappedDiagnosticsContext.Set("ChannelType", ChannelType);
                logger.Log(LogLevel.Trace, "");
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public static void ExceptionLog(ERROR_LOG_WS data)
        {
            try
            {
                logger = LogManager.GetLogger("LogException");

                NLog.MappedDiagnosticsContext.Set("LOG_LEVEL", data.LOG_LEVEL);
                NLog.MappedDiagnosticsContext.Set("SERVICE_NAME", data.SERVICE_NAME);
                NLog.MappedDiagnosticsContext.Set("SERVICE_TYPE", data.SERVICE_TYPE);
                NLog.MappedDiagnosticsContext.Set("ERROR_MESSAGE", data.ERROR_MESSAGE);
                NLog.MappedDiagnosticsContext.Set("SERVER_IP", data.SERVER_IP);
                NLog.MappedDiagnosticsContext.Set("CLIENT_IP ", data.CLIENT_IP);

                logger.Log(LogLevel.Trace, "");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void AuthenticationLog(string UserName, string appID, string LoginStatus)
        {
            try
            {
                logger = LogManager.GetLogger("LogAuthentication");

                NLog.MappedDiagnosticsContext.Set("UserName", UserName);
                NLog.MappedDiagnosticsContext.Set("AppID", appID);
                NLog.MappedDiagnosticsContext.Set("LogInStatus", LoginStatus);


                logger.Log(LogLevel.Trace, "");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

}
