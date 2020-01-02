using GCL_TVS_API.Filters;
using GCL_TVS_API.Models;
using GCL_TVS_API.Process;
using System;
using System.Web.Http;
using static GCL_TVS_API.Models.SODetailsService;
using static GCL_TVS_API.Models.SODetailsUrl;
using static GCL_TVS_API.Models.Token;
using static GCL_TVS_API.Models.TVS;

namespace GCL_TVS_API.Controllers
{
    public class TVSController : ApiController
    {
        private static TVSProcess _process = null;
        private static TVSProcess process
        {
            get { return (_process == null) ? _process = new TVSProcess() : _process; }
        }

        [AllowAnonymous]
        [HttpPost]
        public ResponseInfo<ResponseUrl> GetSODetails([FromBody] RequestUrl data)
        {
            ResponseInfo<ResponseUrl> res = new ResponseInfo<ResponseUrl>();

            try
            {
                res = process.GenerateSoUrl(data);
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return res;
        }
        [AllowAnonymous]
        [HttpPost]
        public dynamic GetDoStatusPage([FromBody] ReqDoUrl data)
        {
            dynamic res = null;

            try
            {
                if (data.Signature_Status == "Y")
                {
                    ReqDo reqDo = new ReqDo();
                    reqDo.DoNo = data.DoNo;
                    res = process.GenerateDoUrl(reqDo);
                }
                else
                {
                    res = new ErrorAuthen();
                    res.status = new StatusError();
                    res.status.code = res.Code;
                    res.status.message = "Signature status is not correct";
                }
                
            }
            catch (Exception ex)
            {
                throw ex; 
            }

            return res;
        }
        [AllowAnonymous]
        [HttpPost]
        public dynamic GetSurveyPage([FromBody] ReqSurveyUrl data)
        {
            dynamic res = null;

            try
            {
                res = process.GenerateSurveyUrl(data);
            }
            catch (Exception ex)
            {
                throw ex; 
            }

            return res;
        }
        [AllowAnonymous]
        [HttpPost]
        public dynamic GetSurveyByHash([FromBody] ReqDataByHash data)
        {
            dynamic res = null;

            try
            {
                res = process.GetSurveyByHash(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }
        [AllowAnonymous]
        [HttpPost]
        public dynamic GetDoByHash([FromBody] ReqDataByHash data)
        {
            dynamic res = null;

            try
            {
                res = process.GetDoByHash(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }
        [AllowAnonymous]
        [HttpPost]
        public ResponseInfo<RspGetGradeList> GetGradeList([FromBody] ReqGetGradeList data)
        {
            ResponseInfo<RspGetGradeList> res = new ResponseInfo<RspGetGradeList>();

            try
            {
                res = process.GetGradeList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }
        [AllowAnonymous]
        [HttpPost]
        public ResponseInfo<ResponseSODetails> GetSODetailsFromHash([FromBody] ReqDataByHash data)
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

        [AllowAnonymous]
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
        [AllowAnonymous]
        [HttpPost]
        public ResponseInfo<RspSystemNotiList> GetSystemNotiList([FromBody] ReqSystemNotiList data)
        {
            ResponseInfo<RspSystemNotiList> res = new ResponseInfo<RspSystemNotiList>();

            try
            {
                res = process.GetSystemNotiList(data);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return res;
        }
        [AllowAnonymous]
        [HttpPost]
        public ResponseInfo<RspJobStatus> GetJobStatus([FromBody] ReqJobStatus data)
        {
            ResponseInfo<RspJobStatus> res = new ResponseInfo<RspJobStatus>();

            try
            {
                res = process.GetJobStatus(data);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return res;
        }
        [AllowAnonymous]
        [HttpPost]
        public ResponseInfo<string> PostSystemNoti([FromBody] ReqPostSystemNoti data)
        {
            ResponseInfo<string> res = new ResponseInfo<string>();

            try
            {
                res = process.PostSystemNoti(data);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return res;
        }
        [AllowAnonymous]
        [HttpPost]
        public ResponseInfo<ResponseCustomerInfo> GetCustomer([FromBody] RequestCustomerInfo data)
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
        [AllowAnonymous]
        [HttpPost]
        public ResponseStatus PostSystemNotiReview([FromBody] NotiReviewObj data)
        {
            ResponseStatus res = new ResponseStatus();

            try
            {
                res = process.PostSystemNotiReview(data);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return res;
        }
        [AllowAnonymous]
        [HttpPost]
        public ResponseStatus PostDOSOMapping([FromBody] DOSOMappingObj data)
        {
            ResponseStatus res = new ResponseStatus();

            try
            {
                res = process.PostDOSOMapping(data);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return res;
        }
        [AllowAnonymous]
        [HttpPost]
        public ResponseStatus PostUpdateCompany([FromBody] UpdCompanyObj data)
        {
            ResponseStatus res = new ResponseStatus();

            try
            {
                res = process.UpdateCompany(data);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return res;
        }
    }
}
