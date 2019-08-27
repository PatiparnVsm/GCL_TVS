using CIMB.DSE.ML.DAL;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static GCL_TVS_API.Models.Token;

namespace GCL_TVS_API.DAL
{
    public class TokenDAL : BaseConnection
    {
        public bool AuthenCheck(RequestToken data)
        {
            ////
            bool result = false;
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    string sql = @"SELECT count(1) FROM [dbo].[SystemsConnect] WHERE [SystemCode] = @SystemCode";

                    if (connection.ExecuteScalar<int>(sql, new { SystemCode = data.systemId, Username = data.userName, Password = data.password }) > 0)
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

        public string GetToken(string systemCode)
        {
            string tokenId = string.Empty;

            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@SystemCode", systemCode);
                    param.Add("TokenID", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
                    connection.Query<string>("SP_GenerateToken", param, commandType: CommandType.StoredProcedure);
                    tokenId = param.Get<string>("TokenID");                   
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
                return tokenId;
            }
        }
    }
}