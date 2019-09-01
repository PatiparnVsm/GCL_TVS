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
            public string responseCode { get; set; }
            public string responseMSG { get; set; }
            public List<SurverListObj> ObjSurverList { get; set; }
        }

        public class SurverListObj
        {
            public string TVSurveylD { get; set; }
            public string SurveylD { get; set; }
            public string SurveySequence { get; set; }
            public string SurveyName { get; set; }
        }

        public class ActivitieList
        {
            public string TokenId { get; set; }
            public string JobOrderID { get; set; }
            public string UserlD { get; set; }            
        }

        public class ResActivitieList
        {
            public string responseCode { get; set; }
            public string responseMSG { get; set; }
            public List<ActivitieListObj> ObjActivitiesList { get; set; }
        }

        public class ActivitieListObj
        {
            public string TVActivitylD { get; set; }
            public string ProcessStatuslD { get; set; }
            public string ProcessStatusSeq { get; set; }
            public string ProcessStatusName { get; set; }
            public string ProcessOn { get; set; }
        }
    }
}