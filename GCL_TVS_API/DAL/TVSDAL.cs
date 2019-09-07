﻿using CIMB.DSE.ML.DAL;
using Dapper;
using GCL_TVS_API.Models;
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
                    string sql = @"  select SysNotiID, MsgTitle, MsgValue, MsgUrl
                                     from SystemNotification
                                     where UserID = @UserID
                                     and IsReview = 0";
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
        public void InsLogReq( string reqParams, string hashParams)
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
                    string sql = @"insert into SystemNotification (UserID, MsgTitle, MsgUrl, CreatedBy, CreatedOn)
                                   values (@UserID, @MsgTitle, @MsgUrl, @SystemName, getdate())
                                        ";
                    var param = new DynamicParameters();
                    param.Add("@UserID", data.UserID);
                    param.Add("@MsgTitle", data.MsgTitle);
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
        



    }
}