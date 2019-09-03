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

        }
        public class ResponsePictureSize
        {
            public string Size { get; set; }
        }
        public class RequestPictureList
        {
            public string JobOrderID { get; set; }

        }
        public class ResponsePictureList
        {
            public List<PictureList> pictures { get; set; }

        }
    }
}