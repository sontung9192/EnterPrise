using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class StringConvertor
    {
        public static int ToInt32(this object str, int defaultvalue)
        {
            if (str == null || str == DBNull.Value)
                return defaultvalue;

            int retval = defaultvalue;

            try
            {
                retval = Convert.ToInt32(str);
            }
            catch (Exception)
            {
                //
                retval = defaultvalue;
            }

            return retval;
        }

        public static long ToInt64(this object str, long defaultvalue)
        {
            if (str == null || str == DBNull.Value)
                return defaultvalue;

            long retval = defaultvalue;

            try
            {
                retval = Convert.ToInt64(str);
            }
            catch (Exception)
            {
                //
                retval = defaultvalue;
            }

            return retval;
        }
    }
}
