using CIMB.DSE.ML.DAL;
using Dapper;
using GCL_TVS_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static GCL_TVS_API.Models.EPOD;

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

        public List<ResSurverList> GetSurveyList(SurverList data)
        {
            List<ResSurverList> ResultSet = new List<ResSurverList>();
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    string sql = @"SELECT a.tvsurveyld, 
                                       a.surveyld, 
                                       b.surveysequence, 
                                       b.surveyname 
                                FROM   truckvisualsurveys AS a, 
                                       mastersurveys AS b 
                                WHERE  a.surveyld = b.surveyld 
                                       AND a.joborderld = @joborderld
                                       AND b.isactive = 1 
                                        ";
                    //ResultSet = connection.Query<SODetails>(sql, param, commandType: CommandType.StoredProcedure).ToList();

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