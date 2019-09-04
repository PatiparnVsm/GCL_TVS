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


        public ResponseInfo<ResponsePictureSize> GetPictureSize()
        {
            ResponseInfo<ResponsePictureSize> response = new ResponseInfo<ResponsePictureSize>();
            try
            {
                response.ResponseData = new ResponsePictureSize();
                response.ResponseData.Size = EPODDAL.GetPicturesize();

                if (string.IsNullOrEmpty(response.ResponseData.Size))
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

        public ResponseInfo<ResponsePictureList> GetPicturesList(RequestPictureList data)
        {
            ResponseInfo<ResponsePictureList> response = new ResponseInfo<ResponsePictureList>();
            try
            {
                response.ResponseData = new ResponsePictureList();
                response.ResponseData.pictures = EPODDAL.GetPicturesList(data);
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
    }
}