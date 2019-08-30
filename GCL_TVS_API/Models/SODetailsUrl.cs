using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCL_TVS_API.Models
{
    public class SODetailsUrl
    {
        public class RequestUrl
        {
            public string tokenId { get; set; }
            public string customerCode { get; set; }
            public string soNo { get; set; }
        }
        public class ResponseUrl
        {
            public string pageUrl { get; set; }
            public string responseCode { get; set; }
            public string responseMSG { get; set; }
        }
    }
}