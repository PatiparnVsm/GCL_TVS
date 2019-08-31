﻿using CIMB.DSE.ML.DAL;
using Dapper;
using GCL_TVS_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static GCL_TVS_API.Models.SODetailsUrl;

namespace GCL_TVS_API.DAL
{
    public class SODAL : BaseConnection
    {
        public bool AuthenCheckTokenExpire(string tokenId)
        {
            ////
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
                    string sql = @"SELECT RequestParam FROM SystemsTokensRequest WHERE ResponseParam = @ResponseParam and TerminateOn >= getdate()";
                    string RequestParam = connection.Query<string>(sql, new { ResponseParam = hashParams }).FirstOrDefault();
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
        public void InsLogReq(string tokenId, string reqParams, string hashParams)
        {
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@TokenID", tokenId);
                    param.Add("@RequestParam", reqParams);
                    param.Add("@ResponseParam", hashParams);
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
        public List<SODetails> GetSODetails(string[] data)
        {
            List<SODetails> ResultSet = new List<SODetails>();
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    var param = new DynamicParameters();
                    for (var i = 0; i < data.Length; i++)
                    {
                        var arrData = data[i].Split(',');
                        param.Add("@" + arrData[0], arrData[1]);
                    }
                    ResultSet = connection.Query<SODetails>("SP_GetJobOrderFromUrl", param, commandType: CommandType.StoredProcedure).ToList();
                    
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
                    param.Add("@TokenID", data.tokenId);
                    param.Add("@SONO", data.soNo);
                    param.Add("@CompanyCode", data.customerCode);
                    res = connection.Query<SODetailsUrl.ResSP>("SP_CheckTokenIdAndSONO", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return res;
        }

    }
}