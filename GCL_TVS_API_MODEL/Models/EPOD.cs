using System;
using System.Collections.Generic;

namespace GCL_TVS_API.Models
{
    public class EPOD
    {
        public class RequestDetailsFromJobOrderID
        {
            public string JobOrderID { get; set; }
        }

        public class RequestJobListFromDriver
        {
            public string UserID { get; set; }
        }

        public class SurverList
        {
            public string JobOrderID { get; set; }
        }

        public class ResSurverList
        {
            public List<SurverListObj> ObjSurverList { get; set; }
        }

        public class SurverListObj
        {
            public string TVSurveyID { get; set; }
            public string SurveyID { get; set; }
            public string SurveySequence { get; set; }
            public string SurveyName { get; set; }
            public Int32 SurveyResult { get; set; }
        }

        public class ActivitieList
        {
            public string JobOrderID { get; set; }
            public string UserID { get; set; }
        }

        public class ResActivitieList
        {
            public List<ActivitieListObj> ObjActivitiesList { get; set; }
        }

        public class ActivitieListObj
        {
            public string TVActivityID { get; set; }
            public string ProcessStatusID { get; set; }
            public string ProcessStatusSeq { get; set; }
            public string ProcessStatusName { get; set; }
            public string ProcessOn { get; set; }
        }

        public class ReqPostTruckVisualActivities
        {
            public string UserID { get; set; }
            public string TVActivityID { get; set; }
            public DateTime? ProcessOn { get; set; }
            public decimal Latitude { get; set; }
            public decimal Longitude { get; set; }
        }

        public class ReqPostTruckVisualPictures
        {
            public string UserID { get; set; }
            public string TVPictureID { get; set; }
            public string PictureImage { get; set; }
        }

        public class PostTruckVisualServeysObj
        {
            public string UserID { get; set; }
            public string TVSurveyID { get; set; }
            public string SurveyAnswerChoice { get; set; }
            public string SurveyAnswerInput { get; set; }

        }
    }
}