using CIMB.DSE.ML.API.Gateway.Controllers;
using GCL_TVS_API.Models;
using GCL_TVS_API_GATEWAY.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static GCL_TVS_API.Models.SODetailsService;
using static GCL_TVS_API.Models.SODetailsUrl;
using static GCL_TVS_API.Models.TVS;

namespace GCL_TVS_API.Controllers
{
    public class TVSController : BaseController
    {
        [JwtAuthentication]
        [HttpPost]
        public ResponseInfo<ResponseUrl> GetSODetails([FromBody] RequestUrl data)
        {
            try
            {
                return base.PostDataToAPINotAuth<ResponseInfo<ResponseUrl>>(base.apiPathAndQuery, data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [AllowAnonymous]
        [HttpPost]
        public ResponseInfo<ResponseSODetails> GetSODetailsFromHash([FromBody] ReqDataByHash data)
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

        [JwtAuthentication]
        [HttpPost]
        public ResponseInfo<ResponseSODetails> GetSoListFromCust([FromBody] RequestSODetailsFromCustAndSo data)
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

        [JwtAuthentication]
        [HttpPost]
        public ResponseInfo<RspSystemNotiList> GetSystemNotiList([FromBody] ReqSystemNotiList data)
        {
            try
            {
                return base.PostDataToAPINotAuth<ResponseInfo<RspSystemNotiList>>(base.apiPathAndQuery, data);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [JwtAuthentication]
        [HttpPost]
        public ResponseInfo<RspJobStatus> GetJobStatus([FromBody] ReqJobStatus data)
        {
            try
            {
                return base.PostDataToAPINotAuth<ResponseInfo<RspJobStatus>>(base.apiPathAndQuery, data);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [JwtAuthentication]
        [HttpPost]
        public ResponseInfo<string> PostSystemNoti([FromBody] ReqPostSystemNoti data)
        {
            try
            {
                return base.PostDataToAPINotAuth<ResponseInfo<string>>(base.apiPathAndQuery, data);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [JwtAuthentication]
        [HttpPost]
        public ResponseInfo<ResponseCustomerInfo> GetCustomer([FromBody] RequestCustomerInfo data)
        {
            try
            {
                return base.PostDataToAPINotAuth<ResponseInfo<ResponseCustomerInfo>>(base.apiPathAndQuery, data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [JwtAuthentication]
        [HttpPost]
        public ResponseInfo<RspGetGradeList> GetGradeList([FromBody] ReqGetGradeList data)
        {
            try
            {
                return base.PostDataToAPINotAuth<ResponseInfo<RspGetGradeList>>(base.apiPathAndQuery, data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [JwtAuthentication]
        [HttpPost]
        public dynamic GetDoStatusPage([FromBody] ReqDoUrl data)
        {
            try
            {
                return base.PostDataToAPINotAuth<dynamic>(base.apiPathAndQuery, data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [JwtAuthentication]
        [HttpPost]
        public dynamic GetSurveyPage([FromBody] ReqSurveyUrl data)
        {
            try
            {
                return base.PostDataToAPINotAuth<dynamic>(base.apiPathAndQuery, data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public dynamic GetSurveyByHash([FromBody] ReqDataByHash data)
        {
            try
            {
                return base.PostDataToAPINotAuth<dynamic>(base.apiPathAndQuery, data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public dynamic GetDoByHash([FromBody] ReqDataByHash data)
        {
            try
            {
                return base.PostDataToAPINotAuth<dynamic>(base.apiPathAndQuery, data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [JwtAuthentication]
        [HttpPost]
        public ResponseStatus PostSystemNotiReview([FromBody] NotiReviewObj data)
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
        [JwtAuthentication]
        [HttpPost]
        public ResponseStatus PostDOSOMapping([FromBody] DOSOMappingObj data)
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
