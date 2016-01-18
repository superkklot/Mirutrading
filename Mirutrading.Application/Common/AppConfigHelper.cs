using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Application.Common
{
	public class AppConfigHelper
	{
		private static readonly string _adminToken = ConfigurationManager.AppSettings["AdminToken"];

		public static string AdminToken { get { return _adminToken; } }
	}
}
