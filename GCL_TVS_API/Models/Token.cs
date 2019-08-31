using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCL_TVS_API.Models
{
    public class Token
    {
        public class RequestToken
        {
            public string systemId { get; set; }
            public string userName { get; set; }
            public string password { get; set; }
        }
        public class ResponseToken
        {
            public string tokenId { get; set; }
        }

        public class ResponseTokenByUser
        {
            public string tokenId { get; set; }
            public string userRole { get; set; }
        }        
    }
}