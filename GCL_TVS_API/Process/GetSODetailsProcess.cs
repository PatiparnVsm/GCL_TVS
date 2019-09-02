﻿using GCL_TVS_API.DAL;
using GCL_TVS_API.Models;
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
                    response.ResponseData.pageUrl = System.Configuration.ConfigurationManager.AppSettings["MasterURL"].ToString() + "?info=" + hashParams;
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
                    response.ResponseData.sODetails = SODAL.GetSODetails(reqParams);

                }
                else
                {
                    response.ResponseCode = "99";
                    response.ResponseMsg = "tokenId expire or invalid";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseInfo<ResponseSODetails> GetdataSOFromCustAndSo(RequestSODetailsFromCustAndSo data)
        {
            ResponseInfo<ResponseSODetails> response = new ResponseInfo<ResponseSODetails>();
            try
            {
                response.ResponseData = new ResponseSODetails();
                response.ResponseData.sODetails = SODAL.GetSODetailsFromCustAndSo(data);

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