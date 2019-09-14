﻿using GCL_TVS_API_MODEL.Models;
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
        public class ReqDoUrl
        {
            public String DoNo { get; set; }
        }
        public class RspSurveyUrl
        {
            public String pageUrl { get; set; }
        }
        public class ReqSurveyUrl
        {
            public String DoNo { get; set; }
        }
        public class RspDoUrl
        {
            public String pageUrl { get; set; }
        }
        public class SystemNotiList
        {
            public Guid? SysNotiID { get; set; }
            public String MsgTitle { get; set; }
            public String MsgValue { get; set; }
            public String MsgUrl { get; set; }
            public String IsReview { get; set; }
            public DateTime? CreatedOn { get; set; }
        }
        public class RspSystemNotiList
        {
            public List<SystemNotiList> systemNotiList { get; set; }
        }
        public class RspSurveyList
        {
            public List<Survey> surveys { get; set; }
        }

        public class ReqJobStatus
        {
            public String JobOrderID { get; set; }
        }
        public class JobStatus
        {
            public Guid? JobOrderID { get; set; }
            public Int32 ProcessStatusID { get; set; }
            public String ProcessStatusName { get; set; }
            public DateTime? ProcessStatusDateTime { get; set; }
            public Boolean IsCompleted { get; set; }
        }
        public class RspJobStatus
        {
            public List<JobStatus> jobStatus { get; set; }
        }
        public class ReqPostSystemNoti
        {
            public string UserID { get; set; }
            public string MsgTitle { get; set; }
            public string MsgValue { get; set; }
            public string MsgUrl { get; set; }
            public string SystemName { get; set; }
        }
        public class ResSPResSP
        {
            public string Code { get; set; }
            public string Msg { get; set; }
        }
    }
}