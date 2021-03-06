﻿using GCL_TVS_API.Filters;
using GCL_TVS_API.Models;
using GCL_TVS_API.Process;
using System;
using System.Collections.Generic;
using System.Web.Http;
using static GCL_TVS_API.Models.EPOD;
using static GCL_TVS_API.Models.Picture;
using static GCL_TVS_API.Models.SODetailsService;

namespace GCL_TVS_API.Controllers
{
    public class EPODController : ApiController
    {
        private static EPODProcess _process = null;
        private static EPODProcess process
        {
            get { return (_process == null) ? _process = new EPODProcess() : _process; }
        }

        [AllowAnonymous]
        [HttpPost]
        public ResponseInfo<RspConfigValue> GetSystemConfig([FromBody] ReqConfigValue data)
        {
            ResponseInfo<RspConfigValue> res = new ResponseInfo<RspConfigValue>();

            try
            {
                res = process.GetSystemConfig(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        //[AllowAnonymous]
        //[HttpPost]
        //public ResponseInfo<ResponsePictureList<string>> GetPicturesList([FromBody] RequestPictureList data)
        //{
        //    ResponseInfo<ResponsePictureList<string>> res = new ResponseInfo<ResponsePictureList<string>>();

        //    try
        //    {
        //        res = process.GetPicturesList(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return res;
        //}

        //[AllowAnonymous]
        //[HttpPost]
        //public ResponseInfo<string> PostTruckVisualActivities([FromBody] ReqPostTruckVisualActivities data)
        //{
        //    ResponseInfo<string> res = new ResponseInfo<string>();

        //    try
        //    {
        //        res = process.PostTruckVisualActivities(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return res;
        //}
        //[AllowAnonymous]
        //[HttpPost]
        //public ResponseInfo<string> PostTruckVisualPictures([FromBody] ReqPostTruckVisualPictures data)
        //{
        //    ResponseInfo<string> res = new ResponseInfo<string>();

        //    try
        //    {
        //        res = process.PostTruckVisualPictures(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return res;
        //}

        //[AllowAnonymous]
        //[HttpPost]
        //public ResponseInfo<ResponseSODetails> GetJobListFromDriver([FromBody] RequestJobListFromDriver data)
        //{
        //    ResponseInfo<ResponseSODetails> res = new ResponseInfo<ResponseSODetails>();

        //    try
        //    {
        //        res = process.GetJobListFromDriver(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return res;
        //}

        [AllowAnonymous]
        [HttpPost]
        public ResponseInfo<ResponseSODetails> GetDetailsFromJobOrderID([FromBody] RequestDetailsFromJobOrderID data)
        {
            ResponseInfo<ResponseSODetails> res = new ResponseInfo<ResponseSODetails>();

            try
            {
                res = process.GetDetailsFromJobOrderID(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        //[AllowAnonymous]
        //[HttpPost]
        //public ResponseInfo<ResSurverList> GetSurveysList([FromBody] SurverList data)
        //{
        //    ResponseInfo<ResSurverList> res = new ResponseInfo<ResSurverList>();

        //    try
        //    {
        //        res = process.GetSurverList(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return res;
        //}

        //[AllowAnonymous]
        //[HttpPost]
        //public ResponseInfo<ResActivitieList> GetActivityList([FromBody] ActivitieList data)
        //{
        //    ResponseInfo<ResActivitieList> res = new ResponseInfo<ResActivitieList>();

        //    try
        //    {
        //        res = process.GetActivitieList(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return res;
        //}

        [AllowAnonymous]
        [HttpPost]
        public ResponseStatus PostTruckVisualSurveys([FromBody] List<PostTruckVisualServeysObj> data)
        {
            ResponseStatus res = new ResponseStatus();

            try
            {
                res = process.PostTruckVisualSurveys(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }
    }
}
