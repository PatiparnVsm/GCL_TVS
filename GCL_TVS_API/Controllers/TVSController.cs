using GCL_TVS_API.Filters;
using GCL_TVS_API.Models;
using GCL_TVS_API.Process;
using System;
using System.Web.Http;
using static GCL_TVS_API.Models.SODetailsService;
using static GCL_TVS_API.Models.SODetailsUrl;

namespace GCL_TVS_API.Controllers
{
    public class TVSController : ApiController
    {
        private static GetSODetailsProcess _process = null;
        private static GetSODetailsProcess process
        {
            get { return (_process == null) ? _process = new GetSODetailsProcess() : _process; }
        }

        [JwtAuthentication]
        [HttpPost]
        public ResponseInfo<ResponseUrl> GetSODetails([FromBody] RequestUrl data)
        {
            ResponseInfo<ResponseUrl> res = new ResponseInfo<ResponseUrl>();

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

        [AllowAnonymous]
        [HttpPost]
        public ResponseInfo<ResponseSODetails> GetSODetailsFromHash([FromBody] RequestSODetails data)
        {
            ResponseInfo<ResponseSODetails>  res = new ResponseInfo<ResponseSODetails>();

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

        [JwtAuthentication]
        [HttpPost]
        public ResponseInfo<ResponseSODetails> GetSoListFromCust([FromBody] RequestSODetailsFromCustAndSo data)
        {
            ResponseInfo<ResponseSODetails> res = new ResponseInfo<ResponseSODetails>();

            try
            {
                res = process.GetSoListFromCust(data);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return res;
        }

        public ResponseInfo<ResponseCustomerInfo> GetCustomerInfo([FromBody] RequestCustomerInfo data)
        {
            ResponseInfo<ResponseCustomerInfo> res = new ResponseInfo<ResponseCustomerInfo>();

            try
            {
                res = process.GetCustomerInfo(data);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return res;
        }


    }
}
