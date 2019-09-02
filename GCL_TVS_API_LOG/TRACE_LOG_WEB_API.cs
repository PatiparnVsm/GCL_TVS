using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIMB.DSE.ML.Logs
{
    public class TRACE_LOG_WEB_API
    {
        public long LogID { get; set; }             // The (database) ID for the API log entry.
        public string Application { get; set; }             // The application that made the request.
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string UserID { get; set; }                    // The user that made the request.
        public string Machine { get; set; }                 // The machine that made the request.
        public string MachineIpAddress { get; set; }
        public string ApiPath { get; set; }
        public string RequestURLParams { get; set; }
        public string RequestIpAddress { get; set; }        // The IP address that made the request.
        public string RequestContentType { get; set; }      // The request content type.
        public string RequestContentBody { get; set; }      // The request content body.
        public string RequestUri { get; set; }              // The request URI.
        public string RequestMethod { get; set; }           // The request method (GET, POST, etc).
        // public string RequestRouteTemplate { get; set; }    // The request route template.
        //  public string RequestRouteData { get; set; }        // The request route data.
        public string RequestHeaders { get; set; }          // The request headers.
        public string RequestTimestamp { get; set; }     // The request timestamp.
        public string ResponseContentType { get; set; }     // The response content type.
        public string ResponseContentBody { get; set; }     // The response content body.
        public int? ResponseStatusCode { get; set; }        // The response status code.
        public string ResponseHeaders { get; set; }         // The response headers.
        public string ResponseTimestamp { get; set; }    // The response timestamp.
        public string ChannelType { get; set; }
    }
}
