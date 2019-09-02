using GCL_TVS_API.Filters;
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
        public ResponsePictureSize GetPictureSize([FromBody] RequestPictureSize data)
        {
            ResponsePictureSize res = new ResponsePictureSize();

            try
            {
                res = process.GetPictureSize(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        [JwtAuthentication]
        [HttpPost]
        public ResponsePictureList GetPicturesList([FromBody] RequestPictureList data)
        {
            ResponsePictureList res = new ResponsePictureList();

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

        [JwtAuthentication]
        [HttpPost]
        public ResponseSODetails GetJobDetailsFromJobnoAndSo([FromBody] RequestJobDetailsFromJobnoAndSo data)
        {
            ResponseSODetails res = new ResponseSODetails();

            try
            {
                res = process.GetdataJobFromCustAndSo(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        [JwtAuthentication]
        [HttpPost]
        public ResSurverList GetSurveysList([FromBody] SurverList data)
        {
            ResSurverList res = new ResSurverList();

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
        public ResActivitieList GetActivityList([FromBody] ActivitieList data)
        {
            ResActivitieList res = new ResActivitieList();

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
