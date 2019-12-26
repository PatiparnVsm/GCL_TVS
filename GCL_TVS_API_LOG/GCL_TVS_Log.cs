using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CIMB.DSE.ML.Logs
{
    public class DSELog
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static string ChannelType = "";

        private static Int64 _traceLogID;
        private static Int64 traceLogID
        {
            get { return _traceLogID; }
        }

        private static string GetConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBSQLConn"].ConnectionString;

        }

        public static void TracingLog(TRACE_LOG_WEB_API data)
        {
            try
            {
                //logger = LogManager.GetLogger("LogReqestResponse");

                //NLog.MappedDiagnosticsContext.Set("Application", data.Application);
                //NLog.MappedDiagnosticsContext.Set("ControllerName", data.ControllerName);
                //NLog.MappedDiagnosticsContext.Set("ActionName", data.ActionName);
                ////NLog.MappedDiagnosticsContext.Set("UserID", data.UserID);
                //NLog.MappedDiagnosticsContext.Set("Machine", data.Machine);
                //NLog.MappedDiagnosticsContext.Set("MachineIpAddress", data.MachineIpAddress);
                //NLog.MappedDiagnosticsContext.Set("RequestIpAddress", data.RequestIpAddress);
                //NLog.MappedDiagnosticsContext.Set("RequestTimestamp", data.RequestTimestamp);
                //NLog.MappedDiagnosticsContext.Set("ResponseTimestamp", data.ResponseTimestamp);
                //NLog.MappedDiagnosticsContext.Set("RequestHeaders", data.RequestHeaders);
                //NLog.MappedDiagnosticsContext.Set("RequestUri", data.RequestUri);
                //NLog.MappedDiagnosticsContext.Set("ApiPath", data.ApiPath);
                //NLog.MappedDiagnosticsContext.Set("RequestContentType", data.RequestContentType);
                //NLog.MappedDiagnosticsContext.Set("RequestMethod", data.RequestMethod);
                //NLog.MappedDiagnosticsContext.Set("RequestURLParams", data.RequestURLParams);
                //NLog.MappedDiagnosticsContext.Set("RequestContentBody", data.RequestContentBody);
                //NLog.MappedDiagnosticsContext.Set("ResponseContentType", data.ResponseContentType);
                //NLog.MappedDiagnosticsContext.Set("ResponseContentBody", data.ResponseContentBody);
                //NLog.MappedDiagnosticsContext.Set("ResponseStatusCode", data.ResponseStatusCode);
                //NLog.MappedDiagnosticsContext.Set("ResponseHeaders", data.ResponseHeaders);
                //NLog.MappedDiagnosticsContext.Set("ChannelType", ChannelType);
                //logger.Log(LogLevel.Trace, "");
            }
            catch (Exception ex)
            {

                throw ex;
            }

            string Query = @" INSERT INTO [dbo].[TRACE_LOG_WEB_API]
                              ([Application],[ControllerName] ,[ActionName] ,[Machine] ,[RequestIpAddress],[RequestTimestamp] ,[ResponseTimestamp],[RequestHeaders],[RequestUri],[ApiPath],[RequestContentType],[RequestMethod],[RequestURLParams] ,[RequestContentBody],[ResponseContentType],[ResponseContentBody],[ResponseStatusCode],[ResponseHeaders],[MachineIpAddress],[ChannelType])
                              VALUES
                              (@Application,@ControllerName,@ActionName,@Machine,@RequestIpAddress,@RequestTimestamp ,@ResponseTimestamp,@RequestHeaders,@RequestUri,@ApiPath,@RequestContentType,@RequestMethod,@RequestURLParams,@RequestContentBody,@ResponseContentType ,@ResponseContentBody,@ResponseStatusCode,@ResponseHeaders,@MachineIpAddress,@ChannelType);
                              SELECT CAST(scope_identity() AS int)";
            try
            {
                List<SqlParameter> listParam = new List<SqlParameter>();
                listParam.Add(new SqlParameter("Application", data.Application));
                listParam.Add(new SqlParameter("ControllerName", data.ControllerName));
                listParam.Add(new SqlParameter("ActionName", data.ActionName));
                //listParam.Add(new SqlParameter("UserID", data.UserID);
                listParam.Add(new SqlParameter("Machine", data.Machine));
                listParam.Add(new SqlParameter("MachineIpAddress", data.MachineIpAddress));
                listParam.Add(new SqlParameter("RequestIpAddress", data.RequestIpAddress));
                listParam.Add(new SqlParameter("RequestTimestamp", data.RequestTimestamp));
                listParam.Add(new SqlParameter("ResponseTimestamp", data.ResponseTimestamp));
                listParam.Add(new SqlParameter("RequestHeaders", data.RequestHeaders));
                listParam.Add(new SqlParameter("RequestUri", data.RequestUri));
                listParam.Add(new SqlParameter("ApiPath", data.ApiPath));
                listParam.Add(new SqlParameter("RequestContentType", data.RequestContentType));
                listParam.Add(new SqlParameter("RequestMethod", data.RequestMethod));
                listParam.Add(new SqlParameter("RequestURLParams", data.RequestURLParams));
                listParam.Add(new SqlParameter("RequestContentBody", data.RequestContentBody));
                listParam.Add(new SqlParameter("ResponseContentType", data.ResponseContentType));
                listParam.Add(new SqlParameter("ResponseContentBody", data.ResponseContentBody));
                listParam.Add(new SqlParameter("ResponseStatusCode", data.ResponseStatusCode));
                listParam.Add(new SqlParameter("ResponseHeaders", data.ResponseHeaders));
                listParam.Add(new SqlParameter("ChannelType", ChannelType));
                _traceLogID = ExecuteScalar(Query, CommandType.Text, listParam);
            }
            catch
            {

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
                NLog.MappedDiagnosticsContext.Set("TRACE_LOG_ID", traceLogID);

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

        private static long ExecuteScalar(string commandText, CommandType cmdType, List<SqlParameter> listSqlParam)
        {
            Int64 result = 0;
            try
            {
                using (SqlConnection _conn = new SqlConnection(GetConnectionString()))
                {
                    _conn.Open();
                    using (SqlCommand _cmd = new SqlCommand(commandText, _conn))
                    {
                        _cmd.CommandType = cmdType;
                        _cmd.CommandTimeout = 0;
                        if (listSqlParam != null)
                        {
                            _cmd.Parameters.Clear();
                            foreach (SqlParameter param in listSqlParam)
                            {
                                if(param.Value == null)
                                {
                                    param.Value = DBNull.Value;
                                }
                                _cmd.Parameters.Add(param);
                            }
                        }

                        var obj = _cmd.ExecuteScalar();
                        if (obj != null)
                        {
                            result = Convert.ToInt64(obj);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }

}
