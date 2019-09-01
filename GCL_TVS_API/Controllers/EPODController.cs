using GCL_TVS_API.Process;
using System;
using System.Web.Http;
using static GCL_TVS_API.Models.EPOD;
using static GCL_TVS_API.Models.SODetailsService;
using static GCL_TVS_API.Models.Token;

namespace GCL_TVS_API.Controllers
{
    public class EPODController : ApiController
    {
        private static EPODProcess _process = null;
        private static EPODProcess process
        {
            get { return (_process == null) ? _process = new EPODProcess() : _process; }
        }

        [HttpPost]
        public ResponseSODetails GetJobDetailsFromJobnoAndSo([FromBody] RequestJobDetailsFromJobnoAndSo data)
        {
            ResponseSODetails res = new ResponseSODetails();

            try
            {
                res = process.GetdataFromJobnoAndSo(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        [HttpPost]
        public ResSurverList GetSurveysList([FromBody] RequestJobDetailsFromJobnoAndSo data)
        {
            ResSurverList res = new ResSurverList();
            try
            {

            }
            catch(Exception ex)
            {

            }
            return res;
        }
    }
}
