using CIMB.DSE.ML.DAL;
using Dapper;
using GCL_TVS_API.Models;
using GCL_TVS_API_MODEL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static GCL_TVS_API.Models.SODetailsService;
using static GCL_TVS_API.Models.SODetailsUrl;
using static GCL_TVS_API.Models.TVS;

namespace GCL_TVS_API.DAL
{
    public class TVSDAL : BaseConnection
    {
        public List<SystemNotiList> GetSystemNotiList(ReqSystemNotiList data)
        {
            List<SystemNotiList> ResultSet = new List<SystemNotiList>();
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    string sql = @"  select SysNotiID, MsgTitle, MsgValue, MsgUrl, IsReview, CreatedOn
                                     from SystemNotification
                                     where UserID = @UserID
                                    ";
                    var param = new DynamicParameters();
                    param.Add("@UserID", data.UserID);
                    ResultSet = connection.Query<SystemNotiList>(sql, param).ToList();
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
                return ResultSet;
            }
        }
        public List<JobStatus> GetJobStatus(ReqJobStatus data)
        {
            List<JobStatus> ResultSet = new List<JobStatus>();
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    string sql = @"SELECT B.JobOrderID, B.ProcessStatusID, C.ProcessStatusName, B.ModifiedOn as ProcessStatusDateTime, B.IsCompleted 
                                   FROM TruckVisualJobOrdersStatus B 
                                   left join MasterProcessStatus C on B.ProcessStatusID = C.ProcessStatusID
                                   WHERE B.JobOrderID = @JobOrderID";
                    var param = new DynamicParameters();
                    param.Add("@JobOrderID", data.JobOrderID);
                    ResultSet = connection.Query<JobStatus>(sql, param).ToList();
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
                return ResultSet;
            }
        }
        public bool AuthenCheckTokenExpire(string tokenId)
        {

            bool result = false;
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    string sql = @"SELECT count(*) FROM [dbo].[SystemsToken] WHERE [TokenID] = @TokenID AND [TerminateOn] >= Getdate()";

                    if (connection.ExecuteScalar<int>(sql, new { TokenID = tokenId }) > 0)
                    {
                        result = true;
                    }
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
            return result;
        }

        public string AuthenCheckTokenURLExpire(string hashParams)
        {
            string result = "";
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    string sql = @"SELECT RequestParam FROM SystemHashURLRequest WHERE HashUrlID = @HashUrlID and TerminateOn >= getdate()";
                    string RequestParam = connection.Query<string>(sql, new { HashUrlID = hashParams }).FirstOrDefault();
                    if (!string.IsNullOrEmpty(RequestParam))
                    {
                        result = RequestParam;
                    }
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
            return result;
        }
        public void InsLogReq(string reqParams, string hashParams)
        {
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@Hash", hashParams);
                    param.Add("@RequestParam", reqParams);
                    param.Add("@CreatedBy", "InsLogReq");
                    connection.Query<string>("SP_InsLogRequest", param, commandType: CommandType.StoredProcedure);
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
        }
        public void PostSystemNoti(ReqPostSystemNoti data)
        {
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    string sql = @"insert into SystemNotification (UserID, MsgTitle,MsgValue, MsgUrl, CreatedBy, CreatedOn)
                                   values (@UserID, @MsgTitle, @MsgValue,@MsgUrl, @SystemName, getdate())
                                        ";
                    var param = new DynamicParameters();
                    param.Add("@UserID", data.UserID);
                    param.Add("@MsgTitle", data.MsgTitle);
                    param.Add("@MsgValue", data.MsgValue);
                    param.Add("@MsgUrl", data.MsgUrl);
                    param.Add("@SystemName", data.SystemName);
                    connection.ExecuteScalar(sql, param, commandType: CommandType.Text);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public List<SODetailsDB> GetSODetails(string[] data)
        {
            List<SODetailsDB> ResultSet = new List<SODetailsDB>();
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    var param = new DynamicParameters();
                    for (var i = 0; i < data.Length; i++)
                    {
                        var arrData = data[i].Split('=');
                        param.Add("@" + arrData[0], arrData[1]);
                    }
                    ResultSet = connection.Query<SODetailsDB>("SP_GetJobOrderFromUrl", param, commandType: CommandType.StoredProcedure).ToList();

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
        public List<SODetailsDB> GetDoByHash(string[] data)
        {
            List<SODetailsDB> ResultSet = new List<SODetailsDB>();
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    var param = new DynamicParameters();
                    for (var i = 0; i < data.Length; i++)
                    {
                        var arrData = data[i].Split('=');
                        param.Add("@" + arrData[0], arrData[1]);
                    }
                    ResultSet = connection.Query<SODetailsDB>("SP_GetDoJobOrderFromUrl", param, commandType: CommandType.StoredProcedure).ToList();

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
        public List<Survey> GetSurveyByHash(string[] data)
        {
            List<Survey> ResultSet = new List<Survey>();
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    var param = new DynamicParameters();
                    for (var i = 0; i < data.Length; i++)
                    {
                        var arrData = data[i].Split('=');
                        param.Add("@" + arrData[0], arrData[1]);
                    }
                    ResultSet = connection.Query<Survey>("SP_GetSurveysListFromUrl", param, commandType: CommandType.StoredProcedure).ToList();

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
        public SODetailsUrl.ResSP ValidateSODetails(RequestUrl data)
        {
            SODetailsUrl.ResSP res = new SODetailsUrl.ResSP();
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@SONO", data.soNo);
                    param.Add("@CompanyCode", data.customerCode);
                    res = connection.Query<SODetailsUrl.ResSP>("SP_CheckSONO", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return res;
        }
        public ResSP ValidateDODetails(ReqDoUrl data)
        {
            ResSP res = new ResSP();
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@DONO", data.DoNo);
                    res = connection.Query<ResSP>("SP_CheckDONO", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return res;
        }
        public ResSP ValidateSurveyByDoNo(ReqSurveyUrl data)
        {
            ResSP res = new ResSP();
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@DONO", data.DoNo);
                    res = connection.Query<ResSP>("SP_CheckSurveyByDoNo", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return res;
        }
        public List<SODetailsDB> GetSoListFromCust(RequestSODetailsFromCustAndSo data)
        {
            List<SODetailsDB> ResultSet = new List<SODetailsDB>();
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@UserType", data.UserType);
                    param.Add("@CustomerCode", data.CustomerCode);
                    param.Add("@LoadingPlanFrom", data.LoadingPlanFrom);
                    param.Add("@LoadingPlanTo", data.LoadingPlanTo);
                    ResultSet = connection.Query<SODetailsDB>("SP_GetSoListFromCust", param, commandType: CommandType.StoredProcedure).ToList();

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

        public List<GradeList> GetGradeList(ReqGetGradeList data)
        {
            List<GradeList> ResultSet = new List<GradeList>();
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@hashValue", data.hashValue);
                    string sql = @" select materialDescription, convert(varchar,qty)+' '+uom as qtyuom 
                                    from TempFromTMS_Orders 
                                    where hashValue = @hashValue
                                    and IsActive = 1";
                    ResultSet = connection.Query<GradeList>(sql, param, commandType: CommandType.StoredProcedure).ToList();

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
        public bool PostSystemNotiReview(NotiReviewObj data)
        {
            bool result = false;
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    string sql = @"UPDATE [SystemNotification] 
                                   SET  [IsReview] = 1
                                   WHERE [SysNotiID] = @SysNotiID
                                        ";
                    var resultUpdate = connection.Execute(sql, new { SysNotiID = data.SysNotiID }, commandType: CommandType.Text);
                    if(resultUpdate > 0)
                    {
                        result = true;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }
        public List<CustomerInfo> GetCustomerInfoList(RequestCustomerInfo data)
        {
            List<CustomerInfo> ResultSet = new List<CustomerInfo>();
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@CustomerCode", data.CustomerCode);
                    ResultSet = connection.Query<CustomerInfo>("SP_GetCustomerInfo", param, commandType: CommandType.StoredProcedure).ToList();

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
        public bool PostDOSOMapping(DOSOMappingObj data)
        {
            bool result = false;
            using (IDbConnection connection = GetOpenConnection())
            {
                var transaction = connection.BeginTransaction();
                try
                {
                    string sql = @"INSERT INTO TempFromEDO 
                                                (TransTypeCode, 
                                                DoNo, 
                                                SoNo,
                                                TruckNo,
                                                ContainerNo,
                                                CreatedBy,
                                                CreatedOn)
                                    VALUES 
                                                (@TransTypeCode,
                                                @DoNo,
                                                @SoNo,
                                                @TruckNo,
                                                @ContainerNo,
                                                'eDO System',
                                                getdate()
                                                )";
                    var resultInsert = connection.Execute(sql, data, transaction, null, CommandType.Text);
                    if (resultInsert > 0)
                    {
                        result = true;
                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    if (connection != null)
                    {
                        connection.Close();
                    }
                }

                return result;
            }
        }
    }
}