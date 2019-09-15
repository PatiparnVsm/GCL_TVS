using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCL_TVS_API.Models
{
    public class ResponseInfo<T> where T : class
    {
        public string ResponseCode { get; set; } = "00";
        public string ResponseMsg { get; set; } = "Success";
        public T ResponseData { get; set; }
    }

    public class ResponseStatus
    {
        public Status status { get; set; }
    }

    public class Status
    {
        public string code { get; set; } = "00";
        public string message { get; set; } = "success";
    }
}