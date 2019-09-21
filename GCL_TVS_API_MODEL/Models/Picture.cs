using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCL_TVS_API.Models
{
    public class Picture
    {
        public class ReqConfigValue
        {
            public string ConfCode { get; set; }
        }
        public class RspConfigValue
        {
            public string SystemConfValue { get; set; }
        }
        public class RequestPictureList
        {
            public string JobOrderID { get; set; }

        }
        public class ResponsePictureList<T> where T : class
        {
            public List<PictureList<T>> pictures { get; set; }

        }
    }
}