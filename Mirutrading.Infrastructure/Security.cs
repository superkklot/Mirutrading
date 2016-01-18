using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Infrastructure
{
	public class Security
	{
		public static string ComputeMd5String(string source)
		{
			using(MD5 md5Hash = MD5.Create())
			{
				byte[] sourceBytes = Encoding.UTF8.GetBytes(source);
				byte[] data = md5Hash.ComputeHash(sourceBytes);
				StringBuilder sb = new StringBuilder();
				foreach(var b in data)
				{
					sb.Append(b.ToString("x2"));
				}
				return sb.ToString();
			}
		}
	}
}
