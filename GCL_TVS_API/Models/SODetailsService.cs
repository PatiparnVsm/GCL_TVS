using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCL_TVS_API.Models
{
    public class SODetailsService
    {
        public class RequestSODetails
        {
            public string hashParams { get; set; }
        }
        public class ResponseSODetails
        {
            public string responseCode { get; set; }
            public List<SODetails> sODetails { get; set; }
            public string responseMSG { get; set; }
        }
    }
}