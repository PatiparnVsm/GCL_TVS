using CIMB.DSE.ML.DAL;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using static GCL_TVS_API.Models.SODetailsUrl;
using static GCL_TVS_API.Models.SODetails;
using GCL_TVS_API.Models;

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
        public void InsLogReq(string tokenId,string reqParams, string hashParams)
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
        public List<SODetails> GetSODetails(string condition)
        {
            List<SODetails> ResultSet = new List<SODetails>();
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    string sql = @" SELECT OrderDate as orderDate,OrderDetails as orderDetails,CompanyName as companyName
                                    FROM SalesOrders A
                                    INNER JOIN Companies B ON A.CompanyID = B.CompanyID
                                    WHERE "+ condition;

                    ResultSet = connection.Query<SODetails>(sql).ToList();
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