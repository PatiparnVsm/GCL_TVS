using GCL_TVS_API.DAL;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using static GCL_TVS_API.Models.SODetailsService;
using static GCL_TVS_API.Models.SODetailsUrl;


namespace GCL_TVS_API.Process
{
    public class GetSODetailsProcess
    {
        private static SODAL _SODAL = null;
        private static SODAL SODAL
        {
            get { return (_SODAL == null) ? _SODAL = new SODAL() : _SODAL; }
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

        public ResponseUrl GenerateUrl(RequestUrl data)
        {
            ResponseUrl response = new ResponseUrl();

            try
            {
                var res = SODAL.ValidateSODetails(data);
                response.responseCode = res.Code;
                response.responseMSG = res.Msg;

                if (response.responseCode == "00")
                {
                    var reqParams = GenerateReqparams(data);
                    string hashParams = Utility.HashData(reqParams);

                    SODAL.InsLogReq(data.tokenId, reqParams, hashParams);

                    hashParams = HttpUtility.UrlEncode(hashParams);
                    response.pageUrl = System.Configuration.ConfigurationManager.AppSettings["MasterURL"].ToString() + "?info=" + hashParams;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }
        public ResponseSODetails GetdataSO(RequestSODetails data)
        {
            ResponseSODetails response = new ResponseSODetails();
            try
            {
                string reqParam = SODAL.AuthenCheckTokenURLExpire(data.hashParams);
                if (!string.IsNullOrEmpty(reqParam))
                {
                    string[] reqParams = reqParam.Split(',');
                    //string condition = reqParams[1] + " and " + reqParams[2];
                    response.sODetails = SODAL.GetSODetails(reqParams);

                    response.responseCode = "00";
                    response.responseMSG = "Success";

                }
                else
                {
                    response.responseCode = "99";
                    response.responseMSG = "tokenId expire or invalid";
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseSODetails GetdataSOFromCustAndSo(RequestSODetailsFromCustAndSo data)
        {
            ResponseSODetails response = new ResponseSODetails();
            try
            {
                if (Utility.IsGuid(data.TokenID) && AuthenDal.ValidateToken(data.TokenID))
                {
                    response.sODetails = SODAL.GetSODetailsFromCustAndSo(data);
                    response.responseCode = "00";
                    response.responseMSG = "Success";
                }
                else
                {
                    response.responseCode = "99";
                    response.responseMSG = "tokenId expire or invalid";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
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