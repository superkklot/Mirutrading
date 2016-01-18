using Mirutrading.Application.Common;
using Mirutrading.WebUI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mirutrading.WebUI.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class MyAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
			var value = CookieHelper.GetCookie(CookieHelper.adminCookieKey);
			if (value == null || !CookieHelper.IsTokenValid(value))
			{
				filterContext.Result = new RedirectResult("~/Admin/Login");
			}
			else
			{
				CookieHelper.RefreshToken(value);
			}
        }
    }
}