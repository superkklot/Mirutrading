using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Mirutrading.WebUI.Common
{
	// for cluster servers, tokens should be stored in a single server.
	public class CookieHelper
	{
		private static Dictionary<string, DateTime> tokens = new Dictionary<string, DateTime>();
		public const string adminCookieKey = "token";
		private const int defaultExpireMinutes = 20;
		private const int defaultCookeExpireDay = 1;
		private const int safeTokenCount = 100;

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
			cookie.Expires = DateTime.Now.AddDays(defaultCookeExpireDay);
			HttpContext.Current.Response.SetCookie(cookie);
		}
		public static string GetNewToken()
		{
			var newToken = Guid.NewGuid();
			string tokenStr = newToken.ToString();
			lock(tokens)
			{
				if (tokens.Count > safeTokenCount)
				{
					Task t = Task.Factory.StartNew((state) => 
					{
						Dictionary<string, DateTime> tmpTokens = state as Dictionary<string, DateTime>;
						if(tmpTokens != null)
						{
							DateTime current = DateTime.Now;
							foreach(var key in tmpTokens.Keys)
							{
								if(tmpTokens[key] < current)
								{
									tmpTokens.Remove(key);
								}
							}
						}
					}, tokens);
					t.Wait();
				}
				tokens.Add(tokenStr, DateTime.Now.AddMinutes(defaultExpireMinutes));
			}
			return tokenStr;
		}
		public static bool IsTokenValid(string token)
		{
			lock (tokens)
			{
				if (tokens.ContainsKey(token))
				{
					var time = tokens[token];
					if (time < DateTime.Now)
					{
						tokens.Remove(token);
						return false;
					}
					else
					{
						return true;
					}
				}
				else
				{
					return false;
				}
			}
		}
		public static void RefreshToken(string token)
		{
			lock (tokens)
			{
				if (tokens.ContainsKey(token))
				{
					tokens[token] = DateTime.Now.AddMinutes(defaultExpireMinutes);
				}
			}
		}
	}
}