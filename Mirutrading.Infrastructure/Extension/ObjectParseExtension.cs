using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class ObjectParseExtension
    {
        public static int ToInt(this object @this)
        {
            int ret = 0;
            if(@this != null && @this != DBNull.Value)
            {
                int.TryParse(@this.ToString(), out ret);
            }
            return ret;
        }

        public static long ToLong(this object @this)
        {
            long ret = 0;
            if(@this != null && @this != DBNull.Value)
            {
                long.TryParse(@this.ToString(), out ret);
            }
            return ret;
        }

        public static string ToString(this object @this)
        {
            string ret = "";
            if(@this != null && @this != DBNull.Value)
            {
                ret = @this.ToString();
            }
            return ret;
        }


    }
}
