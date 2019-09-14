using CIMB.DSE.ML.DAL;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace GCL_TVS_API.DAL
{
    public class SystemDAL : BaseConnection
    {
        public string GetConfig(string configId)
        {
            string ResultSet = string.Empty;
            using (IDbConnection connection = GetOpenConnection())
            {
                try
                {
                    string sql = @"SELECT A.SystemConfValue
                                   FROM SystemConfiguration A
                                   WHERE A.SystemConfCode = @configId AND IsActive = 1
                                        ";
                    ResultSet = connection.Query<string>(sql, new { configId = configId }).FirstOrDefault();

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