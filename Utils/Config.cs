using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class Config
    {
        public static string MakeRefId()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next(999).ToString().PadLeft(3, '0');
        }
        public static SqlConnection getConnectionKhieuNai()
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString.Enterprise"].ConnectionString);
            return sqlConn;
        }
        public static string getConnection()
        {
            var sqlConn = ConfigurationManager.ConnectionStrings["ConnectionString.Enterprise"].ConnectionString;
            return sqlConn;
        }
    }
}
