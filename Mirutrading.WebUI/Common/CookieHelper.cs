using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mirutrading.WebUI.Common
{
	public class CookieHelper
	{
		public const string adminCookieKey = "token";
		public const string adminCookieValue = "3598f66fbc93c0d5abd2dabab9de74cc";

		public static string GetCookie(string key)
		{
			var cookie = HttpContext.Current.Request.Cookies[key];
			if (cookie == null) return null;
			return cookie.Value;
		}
		public static void SetCookie(string key, string value)
		{
			var cookie = new HttpCookie(key, value);
			cookie.HttpOnly = true;
			cookie.Expires = DateTime.Now.AddMinutes(20);
			HttpContext.Current.Response.SetCookie(cookie);
		}
	}
}