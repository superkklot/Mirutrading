using Mirutrading.WebUI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mirutrading.Application;
using Mirutrading.Application.ViewModel.Admin;
using Mirutrading.Application.Interface;
using Mirutrading.WebUI.Common;

namespace Mirutrading.WebUI.Controllers
{
    public class AdminController : Controller
    {
		private IAdminService _adminService;
		private const string _index_url = "~/Admin/Index";

		public AdminController(IAdminService adminService)
		{
			_adminService = adminService;
		}

        // GET: Admin
		[MyAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
			ViewBag.ReturnUrl = _index_url;
            return View();
        }

        [HttpPost]
		[ValidateAntiForgeryToken]
        public ActionResult Login(LoginRequest request, string returnUrl)
        {
			if (!ModelState.IsValid) return View();

			bool isAuthorize = _adminService.IsUserAuthorized(request);
			if(isAuthorize)
			{
				CookieHelper.SetCookie(CookieHelper.adminCookieKey, CookieHelper.GetNewToken());
				return Redirect(returnUrl);
			}
			else
			{
				ModelState.AddModelError("unauthorize", "输入的用户名密码出错");
				ViewBag.ReturnUrl = _index_url;
				return View();
			}
        }
    }
}