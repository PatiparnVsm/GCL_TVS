using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIMB.DSE.ML.DAL
{
    public class BaseConnection
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DBSQLConn"].ConnectionString;

        protected SqlConnection GetOpenConnection(string ConnName = null)
        {
            string connString = string.Empty;
            connString = ConnName == null ? connString = connectionString : ConfigurationManager.ConnectionStrings[ConnName].ConnectionString;
            var connection = new SqlConnection(connString);
            connection.Open();
            return connection;
        }
    }
}
