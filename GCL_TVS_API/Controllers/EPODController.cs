using GCL_TVS_API.Filters;
using GCL_TVS_API.Models;
using GCL_TVS_API.Process;
using System;
using System.Web.Http;
using static GCL_TVS_API.Models.EPOD;
using static GCL_TVS_API.Models.Picture;
using static GCL_TVS_API.Models.SODetailsService;

namespace GCL_TVS_API.Controllers
{
    public class EPODController : ApiController
    {
        private static EPODProcess _process = null;
        private static EPODProcess process
        {
            get { return (_process == null) ? _process = new EPODProcess() : _process; }
        }

        [JwtAuthentication]
        [HttpPost]
        public ResponseInfo<ResponsePictureSize> GetPictureSize()
        {
            ResponseInfo<ResponsePictureSize> res = new ResponseInfo<ResponsePictureSize>();

            try
            {
                res = process.GetPictureSize();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        [JwtAuthentication]
        [HttpPost]
        public ResponseInfo<ResponsePictureList> GetPicturesList([FromBody] RequestPictureList data)
        {
            ResponseInfo<ResponsePictureList> res = new ResponseInfo<ResponsePictureList>();

            try
            {
                res = process.GetPicturesList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        //[JwtAuthentication]
        [AllowAnonymous]
        [HttpPost]
        public ResponseInfo<ResponseSODetails> GetJobListFromDriver([FromBody] RequestJobListFromDriver data)
        {
            ResponseInfo<ResponseSODetails> res = new ResponseInfo<ResponseSODetails>();

            try
            {
                res = process.GetJobListFromDriver(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }
        [JwtAuthentication]
        [HttpPost]
        public ResponseInfo<ResponseSODetails> GetDetailsFromJobOrderID([FromBody] RequestDetailsFromJobOrderID data)
        {
            ResponseInfo<ResponseSODetails> res = new ResponseInfo<ResponseSODetails>();

            try
            {
                res = process.GetDetailsFromJobOrderID(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        [JwtAuthentication]
        [HttpPost]
        public ResponseInfo<ResSurverList> GetSurveysList([FromBody] SurverList data)
        {
            ResponseInfo<ResSurverList> res = new ResponseInfo<ResSurverList>();

            try
            {
                res = process.GetSurverList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        [JwtAuthentication]
        [HttpPost]
        public ResponseInfo<ResActivitieList> GetActivityList([FromBody] ActivitieList data)
        {
            ResponseInfo<ResActivitieList> res = new ResponseInfo<ResActivitieList>();

            try
            {
                res = process.GetActivitieList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }
    }
}
