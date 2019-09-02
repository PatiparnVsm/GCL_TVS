﻿using GCL_TVS_API.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public ResponsePictureSize GetPictureSize(RequestPictureSize data)
        {
            ResponsePictureSize response = new ResponsePictureSize();
            try
            {
                if (Utility.IsGuid(data.TokenID))
                {
                    response.Size = EPODDAL.GetPicturesize(data);

                    if (!string.IsNullOrEmpty(response.Size))
                    {
                        response.responseCode = "00";
                        response.responseMSG = "Success";
                    }
                    else
                    {
                        response.responseCode = "01";
                        response.responseMSG = "Don't have Configuration or Configuration isn't active";
                    }

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

        public ResponsePictureList GetPicturesList(RequestPictureList data)
        {
            ResponsePictureList response = new ResponsePictureList();
            try
            {
                if (Utility.IsGuid(data.TokenID))
                {
                    response.pictures = EPODDAL.GetPicturesList(data);
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

        public ResSurverList GetSurverList(SurverList data)
        {
            ResSurverList res = new ResSurverList();

            try
            {
                if (Utility.IsGuid(data.TokenId))
                {
                    res.ObjSurverList = EPODDAL.GetSurveyList(data);
                    res.responseCode = "00";
                    res.responseMSG = "Success";
                }
                else
                {
                    res.responseCode = "99";
                    res.responseMSG = "tokenId expire or invalid";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }

        public ResActivitieList GetActivitieList(ActivitieList data)
        {
            ResActivitieList res = new ResActivitieList();

            try
            {
                if (Utility.IsGuid(data.TokenId))
                {
                    res.ObjActivitiesList = EPODDAL.GetActivityList(data);
                    res.responseCode = "00";
                    res.responseMSG = "Success";
                }
                else
                {
                    res.responseCode = "99";
                    res.responseMSG = "tokenId expire or invalid";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }

        public ResponseSODetails GetdataJobFromCustAndSo(RequestJobDetailsFromJobnoAndSo data)
        {
            ResponseSODetails response = new ResponseSODetails();
            try
            {
                if (Utility.IsGuid(data.TokenID))
                {
                    response.sODetails = EPODDAL.GetJobDetailsFromJobnoAndSo(data);
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
    }
}