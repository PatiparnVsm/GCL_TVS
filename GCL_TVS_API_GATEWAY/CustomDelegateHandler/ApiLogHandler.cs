using CIMB.DSE.ML.Logs;
using Newtonsoft.Json;
using NLog;
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

namespace CIMB.DSE.ML.API.GATEWAY
{
    public class ApiLogHandler : DelegatingHandler
    {

        public static Logger logger = LogManager.GetCurrentClassLogger();

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
            string requestBody = await request.Content.ReadAsStringAsync();
            logger.Trace(JsonConvert.SerializeObject(apiLogEntry));

            var result = await base.SendAsync(request, cancellationToken);
            return result;

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
                //UserID = _userName, /*context.User.Identity.Name,*/
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