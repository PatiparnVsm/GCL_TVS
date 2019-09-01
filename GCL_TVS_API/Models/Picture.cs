using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCL_TVS_API.Models
{
    public class Picture
    {
        public class RequestPictureSize
        {
            public string TokenID { get; set; }
        }
        public class ResponsePictureSize
        {
            public string responseCode { get; set; }
            public string Size { get; set; }
            public string responseMSG { get; set; }
        }
        public class RequestPictureList
        {
            public string TokenID { get; set; }
            public string JobOrderID { get; set; }

        }
        public class ResponsePictureList
        {
            public string responseCode { get; set; }
            public List<PictureList> pictures { get; set; }
            public string responseMSG { get; set; }

        }
    }
}