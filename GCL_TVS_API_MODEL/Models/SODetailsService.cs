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
            public List<SODetails> sODetails { get; set; }
        }

        public class RequestSODetailsFromCustAndSo
        {
            public string UserType { get; set; }
            public string CustomerCode { get; set; }
        }

        
    }
}