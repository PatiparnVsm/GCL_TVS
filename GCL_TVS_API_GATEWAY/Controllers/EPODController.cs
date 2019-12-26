using CIMB.DSE.ML.API.Gateway.Controllers;
using GCL_TVS_API.Models;
using GCL_TVS_API_GATEWAY.Filters;
using System;
using System.Collections.Generic;
using System.Web.Http;
using static GCL_TVS_API.Models.EPOD;
using static GCL_TVS_API.Models.Picture;
using static GCL_TVS_API.Models.SODetailsService;

namespace GCL_TVS_API.Controllers
{
    public class EPODController : BaseController
    {
        [JwtAuthentication]
        [HttpPost]
        public ResponseInfo<RspConfigValue> GetSystemConfig([FromBody] ReqConfigValue data)
        {
            try
            {
                return base.PostDataToAPINotAuth<ResponseInfo<RspConfigValue>>(base.apiPathAndQuery, data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[JwtAuthentication]
        //[HttpPost]
        //public ResponseInfo<ResponsePictureList<string>> GetPicturesList([FromBody] RequestPictureList data)
        //{
        //    try
        //    {
        //        return base.PostDataToAPINotAuth<ResponseInfo<ResponsePictureList<string>>>(base.apiPathAndQuery, data);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //[JwtAuthentication]
        //[HttpPost]
        //public ResponseInfo<string> PostTruckVisualActivities([FromBody] ReqPostTruckVisualActivities data)
        //{
        //    try
        //    {
        //        return base.PostDataToAPINotAuth<ResponseInfo<string>>(base.apiPathAndQuery, data);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //[JwtAuthentication]
        //[HttpPost]
        //public ResponseInfo<string> PostTruckVisualPictures([FromBody] ReqPostTruckVisualPictures data)
        //{
        //    try
        //    {
        //        return base.PostDataToAPINotAuth<ResponseInfo<string>>(base.apiPathAndQuery, data);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //[JwtAuthentication]
        //[HttpPost]
        //public ResponseInfo<ResponseSODetails> GetJobListFromDriver([FromBody] RequestJobListFromDriver data)
        //{
        //    try
        //    {
        //        return base.PostDataToAPINotAuth<ResponseInfo<ResponseSODetails>>(base.apiPathAndQuery, data);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        [JwtAuthentication]
        [HttpPost]
        public ResponseInfo<ResponseSODetails> GetDetailsFromJobOrderID([FromBody] RequestDetailsFromJobOrderID data)
        {
            try
            {
                return base.PostDataToAPINotAuth<ResponseInfo<ResponseSODetails>>(base.apiPathAndQuery, data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //[JwtAuthentication]
        //[HttpPost]
        //public ResponseInfo<ResSurverList> GetSurveysList([FromBody] SurverList data)
        //{
        //    try
        //    {
        //        return base.PostDataToAPINotAuth<ResponseInfo<ResSurverList>>(base.apiPathAndQuery, data);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //[JwtAuthentication]
        //[HttpPost]
        //public ResponseInfo<ResActivitieList> GetActivityList([FromBody] ActivitieList data)
        //{
        //    try
        //    {
        //        return base.PostDataToAPINotAuth<ResponseInfo<ResActivitieList>>(base.apiPathAndQuery, data);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        [JwtAuthentication]
        [HttpPost]
        public ResponseStatus PostTruckVisualSurveys([FromBody] List<PostTruckVisualServeysObj> data)
        {
            try
            {
                return base.PostDataToAPINotAuth<ResponseStatus>(base.apiPathAndQuery, data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
