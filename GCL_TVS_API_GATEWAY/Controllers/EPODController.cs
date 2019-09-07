using CIMB.DSE.ML.API.Gateway.Controllers;
using GCL_TVS_API.Models;
using System;
using System.Web.Http;
using static GCL_TVS_API.Models.EPOD;
using static GCL_TVS_API.Models.Picture;
using static GCL_TVS_API.Models.SODetailsService;

namespace GCL_TVS_API.Controllers
{
    public class EPODController : BaseController
    {
        ////[JwtAuthentication]
        [AllowAnonymous]
        [HttpPost]
        public ResponseInfo<ResponsePictureSize> GetPictureSize()
        {
            try
            {
                return base.PostDataToAPINotAuth<ResponseInfo<ResponsePictureSize>>(base.apiPathAndQuery, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ////[JwtAuthentication]
        [HttpPost]
        public ResponseInfo<ResponsePictureList<string>> GetPicturesList([FromBody] RequestPictureList data)
        {
            ResponseInfo<ResponsePictureList<string>> res = new ResponseInfo<ResponsePictureList<string>>();

            try
            {
                //res = process.GetPicturesList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        //[JwtAuthentication]
        [HttpPost]
        public ResponseInfo<string> PostTruckVisualActivities([FromBody] ReqPostTruckVisualActivities data)
        {
            ResponseInfo<string> res = new ResponseInfo<string>();

            try
            {
                //res = process.PostTruckVisualActivities(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }
        //[JwtAuthentication]
        [HttpPost]
        public ResponseInfo<string> PostTruckVisualPictures([FromBody] ReqPostTruckVisualPictures data)
        {
            ResponseInfo<string> res = new ResponseInfo<string>();

            try
            {
                //res = process.PostTruckVisualPictures(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        //[JwtAuthentication]
        [HttpPost]
        public ResponseInfo<ResponseSODetails> GetJobListFromDriver([FromBody] RequestJobListFromDriver data)
        {
            ResponseInfo<ResponseSODetails> res = new ResponseInfo<ResponseSODetails>();

            try
            {
                //res = process.GetJobListFromDriver(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        //[JwtAuthentication]
        [HttpPost]
        public ResponseInfo<ResponseSODetails> GetDetailsFromJobOrderID([FromBody] RequestDetailsFromJobOrderID data)
        {
            ResponseInfo<ResponseSODetails> res = new ResponseInfo<ResponseSODetails>();

            try
            {
                //res = process.GetDetailsFromJobOrderID(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        //[JwtAuthentication]
        [HttpPost]
        public ResponseInfo<ResSurverList> GetSurveysList([FromBody] SurverList data)
        {
            ResponseInfo<ResSurverList> res = new ResponseInfo<ResSurverList>();

            try
            {
                //res = process.GetSurverList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        //[JwtAuthentication]
        [HttpPost]
        public ResponseInfo<ResActivitieList> GetActivityList([FromBody] ActivitieList data)
        {
            ResponseInfo<ResActivitieList> res = new ResponseInfo<ResActivitieList>();

            try
            {
                //res = process.GetActivitieList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }
    }
}
