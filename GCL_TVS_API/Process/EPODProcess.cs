using GCL_TVS_API.DAL;
using GCL_TVS_API.Helper;
using GCL_TVS_API.Models;
using System;
using System.Collections.Generic;
using static GCL_TVS_API.Models.EPOD;
using static GCL_TVS_API.Models.Picture;
using static GCL_TVS_API.Models.SODetailsService;

namespace GCL_TVS_API.Process
{
    public class EPODProcess
    {
        private static SystemDAL _SysDAL = null;
        private static SystemDAL SysDAL
        {
            get { return (_SysDAL == null) ? _SysDAL = new SystemDAL() : _SysDAL; }
        }
        private static AuthenDAL _AuthenDal = null;
        private static AuthenDAL AuthenDal
        {
            get { return (_AuthenDal == null) ? _AuthenDal = new AuthenDAL() : _AuthenDal; }
        }
        private static EPODDAL _EPODDAL = null;
        private static EPODDAL EPODDAL
        {
            get { return (_EPODDAL == null) ? _EPODDAL = new EPODDAL() : _EPODDAL; }
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


        public ResponseInfo<RspConfigValue> GetSystemConfig(ReqConfigValue data)
        {
            ResponseInfo<RspConfigValue> response = new ResponseInfo<RspConfigValue>();
            try
            {
                response.ResponseData = new RspConfigValue();
                response.ResponseData.SystemConfValue = SysDAL.GetConfig(data.ConfCode);

                if (string.IsNullOrEmpty(response.ResponseData.SystemConfValue))
                {
                    response.ResponseCode = "01";
                    response.ResponseMsg = "Don't have Configuration or Configuration isn't active";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseInfo<ResponsePictureList<string>> GetPicturesList(RequestPictureList data)
        {
            ResponseInfo<ResponsePictureList<string>> response = new ResponseInfo<ResponsePictureList<string>>();
            ResponsePictureList<byte[]> resultDb = new ResponsePictureList<byte[]>();

            try
            {
                response.ResponseData = new ResponsePictureList<string>();
                resultDb.pictures = EPODDAL.GetPicturesList(data);
                response.ResponseData.pictures = HelperUtil.GeneratePictureListBase64String(resultDb.pictures);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseInfo<ResSurverList> GetSurverList(SurverList data)
        {
            ResponseInfo<ResSurverList> res = new ResponseInfo<ResSurverList>();

            try
            {
                res.ResponseData = new ResSurverList();
                res.ResponseData.ObjSurverList = EPODDAL.GetSurveyList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }
        public ResponseInfo<string> PostTruckVisualActivities(ReqPostTruckVisualActivities data)
        {
            ResponseInfo<string> res = new ResponseInfo<string>();

            try
            {
                EPODDAL.PostTruckVisualActivities(data);
                res.ResponseData = "Success";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }
        public ResponseInfo<string> PostTruckVisualPictures(ReqPostTruckVisualPictures data)
        {
            ResponseInfo<string> res = new ResponseInfo<string>();

            try
            {
                EPODDAL.PostTruckVisualPictures(data);
                res.ResponseData = "Success";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }

        public ResponseInfo<ResActivitieList> GetActivitieList(ActivitieList data)
        {
            ResponseInfo<ResActivitieList> res = new ResponseInfo<ResActivitieList>();

            try
            {
                res.ResponseData = new ResActivitieList();
                res.ResponseData.ObjActivitiesList = EPODDAL.GetActivityList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }

        public ResponseInfo<ResponseSODetails> GetJobListFromDriver(RequestJobListFromDriver data)
        {
            ResponseInfo<ResponseSODetails> response = new ResponseInfo<ResponseSODetails>();
            try
            {
                response.ResponseData = new ResponseSODetails();
                var listData = EPODDAL.GetJobListFromDriver(data);

                response.ResponseData.sODetails = HelperUtil.GenerateSoDetailBase64String(listData);
            }
            catch (Exception ex)

            {
                throw ex;
            }
            return response;
        }

        public ResponseInfo<ResponseSODetails> GetDetailsFromJobOrderID(RequestDetailsFromJobOrderID data)
        {
            ResponseInfo<ResponseSODetails> response = new ResponseInfo<ResponseSODetails>();
            try
            {
                response.ResponseData = new ResponseSODetails();
                var listData = EPODDAL.GetDetailsFromJobOrderID(data);

                response.ResponseData.sODetails = HelperUtil.GenerateSoDetailBase64String(listData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus PostTruckVisualSurveys(List<PostTruckVisualServeysObj> data)
        {
            ResponseStatus res = new ResponseStatus();

            try
            {
                var result = EPODDAL.UpdateTruckVisualSurveys(data);
                if (result)
                {
                    res.status = new Status();
                }
                else
                {
                    res.status = new Status();
                    res.status.code = "01";
                    res.status.message = "can't update data";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }
    }
}