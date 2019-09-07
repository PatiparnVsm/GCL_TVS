using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCL_TVS_API.Models
{
    public class TVS
    {
        public class ReqSystemNotiList
        {
            public String UserID { get; set; }
    }

        public class SystemNotiList
        {
            public Guid SysNotiID { get; set; }
            public String MsgTitle { get; set; }
            public String MsgValue { get; set; }
            public String MsgUrl { get; set; }
        }
        public class RspSystemNotiList
        {
            public List<SystemNotiList> systemNotiList { get; set; }
        }
    }
}