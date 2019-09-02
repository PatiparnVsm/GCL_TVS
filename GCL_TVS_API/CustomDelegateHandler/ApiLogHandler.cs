using CIMB.DSE.ML.Logs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Routing;

namespace CIMB.DSE.ML.API.Internal
{
    public class ApiLogHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var apiLogEntry = CreateApiLogEntryWithRequestData(request);
            if (request.Content != null)
            {
                await request.Content.ReadAsStringAsync()
                    .ContinueWith(task =>
                    {
                        apiLogEntry.RequestContentBody = task.Result;
                    }, cancellationToken);
            }

            return await base.SendAsync(request, cancellationToken)
                .ContinueWith(task =>
                {
                    HttpResponseMessage response = new HttpResponseMessage();
                    try
                    {
                        response = task.Result;
                        apiLogEntry.ResponseStatusCode = (int)response.StatusCode;
                        apiLogEntry.ResponseTimestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss", new CultureInfo("en-US"));

                        if (task.Exception == null)
                        {
                            // Update the API log entry with response info
                            if (response.Content != null)
                            {
                                apiLogEntry.ResponseContentBody = response.Content.ReadAsStringAsync().Result;
                                apiLogEntry.ResponseContentType = response.Content.Headers.ContentType.MediaType;
                                apiLogEntry.ResponseHeaders = SerializeHeaders(response.Content.Headers);
                            }
                        }

                    }
                    finally
                    {
                        // Save the API log to the database : Log Request
                        DSELog.TracingLog(apiLogEntry);
                    }

                    return response;

                }, cancellationToken);
        }

        private TRACE_LOG_WEB_API CreateApiLogEntryWithRequestData(HttpRequestMessage request)
        {
            var context = ((HttpContextBase)request.Properties["MS_HttpContext"]);
            //var routeData = request.GetRouteData();
            var controller = request.GetRouteData().Values["controller"];
            var acction = request.GetRouteData().Values["action"];
            string _userName = "";
            string _appName = "appanme";//System.Web.Configuration.WebConfigurationManager.AppSettings["appName"].ToString();

            var identity = (ClaimsIdentity)context.User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            foreach (var item in claims)
            {
                switch (item.Type)
                {
                    case "userID":
                        _userName = item.Value;
                        break;
                }

            }

            return new TRACE_LOG_WEB_API
            {
                Application = _appName,/*"เดี๋ยวมาใส่ตอนทำ OAuth2",*/
                UserID = _userName, /*context.User.Identity.Name,*/
                Machine = Environment.MachineName,
                MachineIpAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToString(),
                ControllerName = string.Format("{0}", controller),
                ActionName = string.Format("{0}", acction),
                RequestContentType = context.Request.ContentType,
                // RequestRouteTemplate = routeData.Route.RouteTemplate,
                //  RequestRouteData = SerializeRouteData(routeData),
                RequestIpAddress = context.Request.UserHostAddress,
                RequestMethod = request.Method.Method,
                RequestHeaders = SerializeHeaders(request.Headers),
                RequestTimestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss", new CultureInfo("en-US")),
                RequestUri = request.RequestUri.ToString(),
                ApiPath = request.RequestUri.AbsolutePath,
                RequestURLParams = request.RequestUri.Query

            };
        }

        private string SerializeRouteData(IHttpRouteData routeData)
        {
            return JsonConvert.SerializeObject(routeData);
        }

        private string SerializeHeaders(HttpHeaders headers)
        {
            var dict = new Dictionary<string, string>();

            foreach (var item in headers.ToList())
            {
                if (item.Value != null)
                {
                    var header = String.Empty;
                    foreach (var value in item.Value)
                    {
                        header += value + " ";
                    }

                    // Trim the trailing space and add item to the dictionary
                    header = header.TrimEnd(" ".ToCharArray());
                    dict.Add(item.Key, header);
                }
            }

            return JsonConvert.SerializeObject(dict);
        }
    }
}