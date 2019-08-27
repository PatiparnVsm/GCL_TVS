using CIMB.DSE.ML.DAL;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using static GCL_TVS_API.Models.Token;

namespace GCL_TVS_API.DAL
{
    public class TokenDAL : BaseConnection
    {
        public bool AuthenCheck(RequestToken data)
        {
            bool result = false;
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    string sql = @"SELECT count(1) FROM [dbo].[SystemsConnect] WHERE [SystemCode] = @SystemCode AND [Username] = @Username AND [Password] = @Password [Item_Flag] = 1";

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
    }
}