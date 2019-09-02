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
        }
        public class ResponseToken
        {
            public string tokenId { get; set; }
        }

        public class AuthenByUser
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public class ResponseTokenByUser
        {
            public string TokenID { get; set; }
            public string UserID { get; set; }
            public string UserType { get; set; }
            public string FullName { get; set; }
        }
    }
}