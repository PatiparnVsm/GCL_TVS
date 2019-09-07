using GCL_TVS_API.DAL;
using GCL_TVS_API.Helper;
using GCL_TVS_API.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using static GCL_TVS_API.Models.SODetailsService;
using static GCL_TVS_API.Models.SODetailsUrl;
using static GCL_TVS_API.Models.TVS;

namespace GCL_TVS_API.Process
{
    public class TVSProcess
    {
        private static TVSDAL _SODAL = null;
        private static TVSDAL SODAL
        {
            get { return (_SODAL == null) ? _SODAL = new TVSDAL() : _SODAL; }
        }

        private static AuthenDAL _AuthenDal = null;
        private static AuthenDAL AuthenDal
        {
            get { return (_AuthenDal == null) ? _AuthenDal = new AuthenDAL() : _AuthenDal; }
        }

        private static Utility _Utility = null;
        private static Utility Utility
        {
            get { return (_Utility == null) ? _Utility = new Utility() : _Utility; }
        }

        private static HelperUtil _HelperUtil = null;
        private static HelperUtil HelperUtil
        {
            get { return (_HelperUtil == null) ? _HelperUtil = new HelperUtil() : _HelperUtil; }
        }

        public ResponseInfo<ResponseUrl> GenerateUrl(RequestUrl data)
        {
            ResponseInfo<ResponseUrl> response = new ResponseInfo<ResponseUrl>();

            try
            {
                var res = SODAL.ValidateSODetails(data);
                response.ResponseCode = res.Code;
                response.ResponseMsg = res.Msg;
                //00 = Success,01 = Not found SalesOrders or CustomerCode
                if (response.ResponseCode == "00")
                {
                    var reqParams = GenerateReqparams(data);
                    string hashParams = Utility.HashData(Guid.NewGuid().ToString());

                    SODAL.InsLogReq(reqParams, hashParams);

                    hashParams = HttpUtility.UrlEncode(hashParams);
                    response.ResponseData = new ResponseUrl();
                    response.ResponseData.pageUrl = System.Configuration.ConfigurationManager.AppSettings["MasterURL"].ToString() + hashParams;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }
        public ResponseInfo<ResponseSODetails> GetdataSO(RequestSODetails data)
        {
            ResponseInfo<ResponseSODetails> response = new ResponseInfo<ResponseSODetails>();
            try
            {
                string reqParam = SODAL.AuthenCheckTokenURLExpire(data.hashParams);
                if (!string.IsNullOrEmpty(reqParam))
                {
                    string[] reqParams = reqParam.Split(',');
                    response.ResponseData = new ResponseSODetails();

                    var listData = SODAL.GetSODetails(reqParams);

                    response.ResponseData.sODetails = HelperUtil.GenerateSoDetailBase64String(listData);

                }
                else
                {
                    response.ResponseCode = "99";
                    response.ResponseMsg = "HashParams expire or invalid";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseInfo<ResponseSODetails> GetSoListFromCust(RequestSODetailsFromCustAndSo data)
        {
            ResponseInfo<ResponseSODetails> response = new ResponseInfo<ResponseSODetails>();
            try
            {

                response.ResponseData = new ResponseSODetails();

                var listData = SODAL.GetSoListFromCust(data);

                response.ResponseData.sODetails = HelperUtil.GenerateSoDetailBase64String(listData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseInfo<ResponseCustomerInfo> GetCustomerInfo(RequestCustomerInfo data)
        {
            ResponseInfo<ResponseCustomerInfo> response = new ResponseInfo<ResponseCustomerInfo>();
            try
            {
                response.ResponseData = new ResponseCustomerInfo();
                response.ResponseData.ResCustomerInfo = SODAL.GetCustomerInfoList(data);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
        public ResponseInfo<RspSystemNotiList> GetSystemNotiList(ReqSystemNotiList data)
        {
            ResponseInfo<RspSystemNotiList> response = new ResponseInfo<RspSystemNotiList>();
            try
            {
                response.ResponseData = new RspSystemNotiList();
                response.ResponseData.systemNotiList = SODAL.GetSystemNotiList(data);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
        public ResponseInfo<RspJobStatus> GetJobStatus(ReqJobStatus data)
        {
            ResponseInfo<RspJobStatus> response = new ResponseInfo<RspJobStatus>();
            try
            {
                response.ResponseData = new RspJobStatus();
                response.ResponseData.jobStatus = SODAL.GetJobStatus(data);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
        public ResponseInfo<string> PostSystemNoti(ReqPostSystemNoti data)
        {
            ResponseInfo<string> res = new ResponseInfo<string>();

            try
            {
                SODAL.PostSystemNoti(data);
                res.ResponseData = "Success";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }

        private string GenerateReqparams(object data)
        {
            string reqParam = string.Empty;
            Type myType = data.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

            var reqParams = string.Empty;
            foreach (PropertyInfo prop in props)
            {
                var name = prop.Name;
                var value = (string)prop.GetValue(data, null);

                reqParams += string.Format(name + "=" + value + ",");
            }
            reqParams = reqParams.Remove(reqParams.Length - 1);
            return reqParams;
        }
    }
}