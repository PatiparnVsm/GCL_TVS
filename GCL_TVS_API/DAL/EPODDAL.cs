using CIMB.DSE.ML.DAL;
using Dapper;
using GCL_TVS_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using static GCL_TVS_API.Models.EPOD;
using static GCL_TVS_API.Models.Picture;

namespace GCL_TVS_API.DAL
{
    public class EPODDAL : BaseConnection
    {
        public List<SODetailsDB> GetJobListFromDriver(RequestJobListFromDriver data)
        {
            List<SODetailsDB> ResultSet = new List<SODetailsDB>();
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@UserID", data.UserID);
                    ResultSet = connection.Query<SODetailsDB>("SP_GetJobListFromDriver", param, commandType: CommandType.StoredProcedure).ToList();

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
        public List<SODetailsDB> GetDetailsFromJobOrderID(RequestDetailsFromJobOrderID data)
        {

            List<SODetailsDB> ResultSet = new List<SODetailsDB>();
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@JobOrderID", data.JobOrderID);
                    ResultSet = connection.Query<SODetailsDB>("SP_GetDetailsFromJobOrderID", param, commandType: CommandType.StoredProcedure).ToList();

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
                    string sql = @"SELECT a.TVSurveyID, 
                                       a.SurveyID, 
                                       b.SurveySequence, 
                                       b.SurveyName 
                                FROM   TruckVisualSurveys  AS a, 
                                       MasterSurveys  AS b 
                                WHERE  a.SurveyID = b.SurveyID 
                                       AND a.JobOrderID = @JobOrderID
                                       AND b.IsActive = 1 
                                        ";
                    ResultSet = connection.Query<SurverListObj>(sql, new { JobOrderID = data.JobOrderID }, commandType: CommandType.Text).ToList();

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
                    string sql = @"select a.TVActivityID, a.ProcessStatusID, b.ProcessStatusSeq,
                                            b.ProcessStatusName, a.ProcessOn
                                   from TruckVisualActivities as a, MasterProcessStatus as b, Users as c
                                   where a.ProcessStatusID = b.ProcessStatusID
                                            And a.DriverID = c.DriverID
                                            And a.JobOrderID = @JobOrderID
                                            And c.UserID = @UserID 
                                        ";
                    ResultSet = connection.Query<ActivitieListObj>(sql, new { JobOrderID = data.JobOrderID, UserID = data.UserID }, commandType: CommandType.Text).ToList();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return ResultSet;
        }
        public void PostTruckVisualActivities(ReqPostTruckVisualActivities data)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    string sql = @" update TruckVisualActivities
                                    set ProcessOn = @ProcessOn,
                                    Latitude = @Latitude,
                                    Longitude = @Longitude,
                                    ModifiedBy = @UserID,
                                    ModifiedOn = getdate()
                                    where TVActivityID = @TVActivityID
                                    and ModifiedBy Is null
                                        ";
                    var ProcessOn = string.Empty;
                    if (data.ProcessOn.HasValue)
                    {
                        ProcessOn = data.ProcessOn.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    }
                    else
                    {
                        ProcessOn = null;
                    }

                    connection.ExecuteScalar(sql, new { ProcessOn = ProcessOn, Latitude = data.Latitude, Longitude = data.Longitude, UserID = data.UserID, TVActivityID = data.TVActivityID }, commandType: CommandType.Text);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public void PostTruckVisualPictures(ReqPostTruckVisualPictures data)
        {
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    string sql = @" update TruckVisualPictures
                                    set PictureImage = @PictureImage,
                                    ModifiedBy = @UserID, 
                                    ModifiedOn = getdate()
                                    Where TVPictureID = @TVPictureID
                                    and PictureApprovedStatus = 0
                                        ";
                    connection.ExecuteScalar(sql, new { PictureImage = Convert.FromBase64String(data.PictureImage), UserID = data.UserID, TVPictureID = data.TVPictureID }, commandType: CommandType.Text);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public string GetPicturesize()
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

        public List<PictureList<byte[]>> GetPicturesList(RequestPictureList data)
        {
            List<PictureList<byte[]>> ResultSet = new List<PictureList<byte[]>>();
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
                    ResultSet = connection.Query<PictureList<byte[]>>(sql, new { JobOrderID = data.JobOrderID }).ToList();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return ResultSet;
        }

        public bool UpdateTruckVisualSurveys(PostTruckVisualServeysObj data)
        {
            bool result;
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    string sql = @"UPDATE TruckVisualSurveys 
                                   SET  SurveyResult = @SurveyResult,
                                        ModifiedBy = @UserID,
                                        ModifiedOn = getdate()
                                   WHERE TVSurveyID = @TVSurveyID
                                        ";
                    result = connection.ExecuteScalar<bool>(sql, new { SurveyResult = data.SurveyResult, UserID = data.UserID, TVSurveyID = data.TVSurveyID }, commandType: CommandType.Text);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }
    }
}