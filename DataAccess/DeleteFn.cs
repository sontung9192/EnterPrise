using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Utils;
namespace DataAccess
{
    public class DeleteFn : DbHelper
    {
        static string responseMessage = "Lỗi hệ thống";
        static string content = "";
        readonly DbHelper _db = new DbHelper(Config.getConnection());
        #region Delete common
        public int Delele(int? Id, string nameStore)
        {
            try
            {

                //var conn = Config.getConnectionFn();
                //conn.Open();
                //SqlCommand cmd = new SqlCommand(nameStore, conn);
                //cmd.CommandType = CommandType.StoredProcedure;
                //SqlParameter[] parms =
                //{
                //    new SqlParameter("@_Id",SqlDbType.Int){Value=Id},
                //    new SqlParameter("@_ResponseCode",SqlDbType.Int){Direction=ParameterDirection.Output}
                //};
                //cmd.Parameters.AddRange(parms);
                //cmd.ExecuteNonQuery();
                //var Code = -1;
                //var temp = cmd.Parameters["@_ResponseCode"].Value;
                //if (temp != null)
                //{
                //    Code = Convert.ToInt32(temp);
                //}
                //conn.Close();
                //conn.Dispose();
                //return Code;
                List<SqlParameter> parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@_Id", SqlDbType.Int) { Value =Id },
                    };
                var output = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                parameters.Add(output);
                var data = _db.ExecuteNonQuerySP(nameStore, parameters.ToArray());
                int temp = -1;
                if (output.Value != null)
                {
                    temp = int.Parse(output.Value.ToString());
                }
                return temp;
            }
            catch (Exception ex)
            {
                LogHelper.LogInfo(String.Format("Error[" + nameStore + "]:{ 0}", ex.Message));
                return -100;
            }
        }

        public  int DelteleLong(long? Id, string nameStore)
        {
		try
            {
            // var conn = Config.getConnectionFn();
            // conn.Open();
            // SqlCommand cmd = new SqlCommand(nameStore, conn);
            // cmd.CommandType = CommandType.StoredProcedure;
            // SqlParameter[] parms =
            // {
                // new SqlParameter("@_Id",SqlDbType.BigInt){Value=Id},
                // new SqlParameter("@_ResponseCode",SqlDbType.Int){Direction=ParameterDirection.Output}
            // };
            // cmd.Parameters.AddRange(parms);
            // cmd.ExecuteNonQuery();
            // var Code = -1;
            // var temp = cmd.Parameters["@_ResponseCode"].Value;
            // if (temp != null)
            // {
                // Code = Convert.ToInt32(temp);
            // }
            // conn.Close();
            // conn.Dispose();
            // return Code;
			 List<SqlParameter> parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@_Id", SqlDbType.BigInt) { Value =Id },
                    };
                var output = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                parameters.Add(output);
                var data = _db.ExecuteNonQuerySP(nameStore, parameters.ToArray());
                int temp = -1;
                if (output.Value != null)
                {
                    temp = int.Parse(output.Value.ToString());
                }
                return temp;
			 }
            catch (Exception ex)
            {
                LogHelper.LogInfo(String.Format("Error[" + nameStore + "]:{ 0}", ex.Message));
                return -100;
            }
        }
        #endregion
    }
}
