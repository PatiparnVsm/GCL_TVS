using CIMB.DSE.ML.DAL;
using Dapper;
using System;
using System.Data;
using static GCL_TVS_API.Models.Token;

namespace GCL_TVS_API.DAL
{
    public class AuthenDAL : BaseConnection
    {      

        public string GetToken(string systemCode)
        {
            string tokenId = string.Empty;

            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@SystemID", systemCode);
                    param.Add("TokenId", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
                    connection.Query<string>("SP_GenerateTokenBySystemId", param, commandType: CommandType.StoredProcedure);
                    tokenId = param.Get<string>("TokenId");
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

        public string GetTokenbyUser(string username, string password)
        {
            string tokenId = string.Empty;

            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@Username", username);
                    param.Add("@Password", password);
                    param.Add("TokenId", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
                    connection.Query<string>("SP_GenerateTokenByUser", param, commandType: CommandType.StoredProcedure);
                    tokenId = param.Get<string>("TokenId");
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

        public bool ValidateToken(string token)
        {
            bool result = false;
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    string sql = @"SELECT dbo.fn_CheckTokenId(@token)";

                    var resultCode = connection.ExecuteScalar<string>(sql, new { token = token });
                    if (resultCode == "00")
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
    }
}