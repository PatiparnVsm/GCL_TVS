using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCL_TVS_API.Models
{
    public class EPOD
    {
        public class RequestJobDetailsFromJobnoAndSo
        {
            public string TokenID { get; set; }
            public string UserID { get; set; }
            public string JobNo { get; set; }
            public string SoNo { get; set; }
        }

        public class SurverList
        {
            public string TokenId { get; set; }
            public string JobOrderID { get; set; }
        }
        public class ResSurverList
        {
            public string TokenId { get; set; }
            public string JobOrderID { get; set; }
            public string UserID { get; set; }
        }
    }
}