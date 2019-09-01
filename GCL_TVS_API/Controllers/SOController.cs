using GCL_TVS_API.Process;
using System;
using System.Web.Http;
using static GCL_TVS_API.Models.SODetailsService;
using static GCL_TVS_API.Models.SODetailsUrl;

namespace GCL_TVS_API.Controllers
{
    public class SOController : ApiController
    {
        private static GetSODetailsProcess _process = null;
        private static GetSODetailsProcess process
        {
            get { return (_process == null) ? _process = new GetSODetailsProcess() : _process; }
        }

        [HttpPost]
        public ResponseUrl GetSODetails([FromBody] RequestUrl data)
        {
            ResponseUrl res = new ResponseUrl();

            try
            {
                res = process.GenerateUrl(data);
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return res;
        }
        [HttpPost]
        public ResponseSODetails GetSODetailsFromHash([FromBody] RequestSODetails data)
        {
            ResponseSODetails res = new ResponseSODetails();

            try
            {
                res = process.GetdataSO(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        [HttpPost]
        public ResponseSODetails GetSoDetailsFromCustAndSo([FromBody] RequestSODetailsFromCustAndSo data)
        {
            ResponseSODetails res = new ResponseSODetails();

            try
            {
                res = process.GetdataSOFromCustAndSo(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        [HttpPost]
        public ResponseSODetails GetSoDetailsFromJobnoAndSo([FromBody] RequestSODetailsFromJobnoAndSo data)
        {
            ResponseSODetails res = new ResponseSODetails();

            try
            {
                res = process.GetdataSOFromJobnoAndSo(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }
    }
}
