using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using System.Data;
using System.Data.SqlClient;
using Models;
namespace DataAccess
{
    public class TruyenDuLieuBLL : DbHelper
    {
        readonly DbHelper _db = new DbHelper(Config.getConnection());
        //public DataTable TruyenDuLieu_GetAllByTop(int top)
        //{
        //    try
        //    {
        //        List<SqlParameter> parameters = new List<SqlParameter>
        //            {
        //                new SqlParameter("@_Top", SqlDbType.Int) { Value = top },

        //            };
        //        var data = _db.GetDataTableSP("sp_TruyenDuLieu_GetAll", parameters.ToArray());
        //        return data;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.LogInfo(string.Format("[DAL][sp_TruyenDuLieu_GetAll],ex={0}", ex.Message));
        //        return null;
        //    }
        //}
        //public List<TruyenDuLieuDTO> TruyenDuLieu_GetAllByTop_Test(int top, ref int total)
        //{
        //    try
        //    {
        //        List<SqlParameter> parameters = new List<SqlParameter>
        //            {
        //                new SqlParameter("@_Top", SqlDbType.Int) { Value = top },

        //            };
        //        var output = new SqlParameter("@_TotalRecord", SqlDbType.Int) { Direction = ParameterDirection.Output };
        //        parameters.Add(output);
        //        var data = _db.GetListSP<TruyenDuLieuDTO>("sp_TruyenDuLieu_GetAll", parameters.ToArray());
        //        total = 0;
        //        if (output.Value != null)
        //        {
        //            total = int.Parse(output.Value.ToString());
        //        }
        //        return data;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.LogInfo(string.Format("[DAL][sp_TruyenDuLieu_GetAll],ex={0}", ex.Message));
        //        return null;
        //    }
        //}
    }
}
