using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCL_TVS_API.Models
{
    public class ResponseInfo<T> where T : class
    {
        public string ResponseCode { get; set; } = "000";
        public string ResponseMsg { get; set; } = "Success";
        public T ResponseData { get; set; }
    }
}