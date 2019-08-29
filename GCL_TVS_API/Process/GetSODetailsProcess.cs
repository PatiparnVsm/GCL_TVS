using GCL_TVS_API.DAL;
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
        private static Utility _Utility = null;
        private static Utility Utility
        {
            get { return (_Utility == null) ? _Utility = new Utility() : _Utility; }
        }

        public ResponseUrl GenerateUrl(RequestUrl data)
        {
            ResponseUrl response = new ResponseUrl();

            var responseCode = SODAL.ValidateSODetails(data);
            response.responseCode = responseCode;

            if (responseCode == "00")
            {
                string reqParams = string.Format("TokenID='{0}',CompanyCode='{1}',SONO='{2}'", data.tokenId, data.customerCode, data.soNo);
                string hashParams = Utility.HashData(reqParams);
                hashParams = HttpUtility.UrlEncode(hashParams);

                response.soUrl = System.Configuration.ConfigurationManager.AppSettings["MasterURL"].ToString() + "?info=" + hashParams;
                response.responseMSG = "Success";

                SODAL.InsLogReq(data.tokenId, reqParams, hashParams);
            }
            if (responseCode == "01")
            {
                response.responseMSG = "Not found SalesOrders or CustomerCode";
            }
            if (responseCode == "99")
            {
                response.responseMSG = "tokenId expire or invalid";
            }
            //if (SODAL.AuthenCheckTokenExpire(data.tokenId))
            //{
            //    string reqParams = string.Format("TokenID='{0}',CompanyCode='{1}',SONO='{2}'", data.tokenId,data.customerCode,data.soNo);
            //    string hashParams = Utility.HashData(reqParams);
            //    response.responseCode = "000";
            //    hashParams = HttpUtility.UrlEncode(hashParams);
            //    response.soUrl = System.Configuration.ConfigurationManager.AppSettings["MasterURL"].ToString() + "?info=" + hashParams;
            //    response.responseMSG = "Success";
            //    SODAL.InsLogReq(data.tokenId, reqParams, hashParams);
            //}
            //else
            //{
            //    response.responseCode = "99";
            //    response.soUrl = "";
            //    response.responseMSG = "tokenId expire or invalid";
            //}

            return response;
        }
        public ResponseSODetails GetdataSO(RequestSODetails data)
        {
            ResponseSODetails response = new ResponseSODetails();
            string reqParam = SODAL.AuthenCheckTokenURLExpire(data.hashParams);
            if (!string.IsNullOrEmpty(reqParam))
            {
                string[] reqParams = reqParam.Split(',');
                string condition = reqParams[1] + " and " + reqParams[2];
                response.responseCode = "000";
                response.sODetails = SODAL.GetSODetails(condition);
                response.responseMSG = "Success";

            }
            else
            {
                response.responseCode = "99";
                response.responseMSG = "tokenId expire or invalid";
            }

            return response;
        }
    }
}