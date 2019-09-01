using CIMB.DSE.ML.DAL;
using Dapper;
using GCL_TVS_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static GCL_TVS_API.Models.EPOD;
using static GCL_TVS_API.Models.Picture;

namespace GCL_TVS_API.DAL
{
    public class EPODDAL : BaseConnection
    {
        public List<SODetails> GetJobDetailsFromJobnoAndSo(RequestJobDetailsFromJobnoAndSo data)
        {
            List<SODetails> ResultSet = new List<SODetails>();
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@UserID", data.UserID);
                    param.Add("@JobNo", data.JobNo);
                    param.Add("@SoNo", data.SoNo);
                    ResultSet = connection.Query<SODetails>("SP_GetJobDetailsFromJobnoAndSo", param, commandType: CommandType.StoredProcedure).ToList();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connection != null)
                    {
                        connection.Close();
                    }
                }
            }

            return ResultSet;

        }

        public List<SurverListObj> GetSurveyList(SurverList data)
        {
            List<SurverListObj> ResultSet = new List<SurverListObj>();
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    string sql = @"SELECT a.TVSurveylD, 
                                       a.SurveylD, 
                                       b.SurveySequence, 
                                       b.SurveyName 
                                FROM   TruckVisualSurveys  AS a, 
                                       MasterSurveys  AS b 
                                WHERE  a.SurveylD = b.SurveylD 
                                       AND a.JobOrderlD = @JobOrderlD
                                       AND b.IsActive = 1 
                                        ";
                    ResultSet = connection.Query<SurverListObj>(sql, new { JobOrderlD = data.JobOrderID}, commandType: CommandType.StoredProcedure).ToList();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return ResultSet;
        }

        public List<ActivitieListObj> GetActivityList(ActivitieList data)
        {
            List<ActivitieListObj> ResultSet = new List<ActivitieListObj>();
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    string sql = @"select a.TVActivitylD, a.ProcessStatuslD, b.ProcessStatusSeq,
                                            b.ProcessStatusName, a.ProcessOn
                                   from TruckVisualActivities as a, MasterProcessStatus as b, Users as c
                                   where a.ProcessStatuslD = b.ProcessStatuslD
                                            And a.DriverlD = c.DriverlD
                                            And a.JobOrderlD = @JobOrderlD
                                            And c.UserlD = @UserlD 
                                        ";
                    ResultSet = connection.Query<ActivitieListObj>(sql, new { JobOrderlD = data.JobOrderID, UserlD = data.UserlD }, commandType: CommandType.StoredProcedure).ToList();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return ResultSet;
        }
        public string GetPicturesize(RequestPictureSize data)
        {
            string result = "";
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    string sql = @"SELECT A.SystemConfValue
                                   FROM SystemConfiguration A
                                   WHERE A.SystemConfCode = 2001 AND IsActive = 1
                                        ";
                    string RequestParam = connection.Query<string>(sql).FirstOrDefault();
                    if (!string.IsNullOrEmpty(RequestParam))
                    {
                        result = RequestParam;
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }

        public List<PictureList> GetPicturesList(RequestPictureList data)
        {
            List<PictureList> ResultSet = new List<PictureList>();
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    string sql = @"SELECT A.TVPictureID,A.PictureID,B.PictureSequence,B.PictureName
                                   FROM TruckVisualPictures A
                                   INNER JOIN MasterPictures B ON A.PictureID = B.PictureID
                                   WHERE a.JobOrderID = @JobOrderID
                                   AND b.IsActive = 1 
                                        ";
                    ResultSet = connection.Query<PictureList>(sql, new { JobOrderID = data.JobOrderID }).ToList();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return ResultSet;
        }
    }
}