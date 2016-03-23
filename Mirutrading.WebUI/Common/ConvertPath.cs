using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mirutrading.WebUI.Common
{
    public class ConvertPath
    {
        public static string Map(string vpath)
        {
            var appPath = HttpContext.Current.Request.ApplicationPath;
            return string.Format("{0}{1}", appPath, vpath.Substring(1));
        }
    }
}