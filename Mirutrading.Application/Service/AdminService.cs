using Mirutrading.Application.Common;
using Mirutrading.Application.Interface;
using Mirutrading.Application.ViewModel.Admin;
using Mirutrading.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Application.Service
{
	public class AdminService : IAdminService
	{
		public bool IsUserAuthorized(LoginRequest request)
		{
			var targetToken = AppConfigHelper.AdminToken;
			var source = string.Format("{0}_{1}", request.UserName, request.Password);
			var sourceToken = Security.ComputeMd5String(source);
			return targetToken == sourceToken;
		}
	}
}
