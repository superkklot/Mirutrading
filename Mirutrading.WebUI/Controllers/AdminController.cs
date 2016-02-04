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
using Mirutrading.Application.ViewModel;
using Mirutrading.Infrastructure.ExceptionHandling;
using System.Text;

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
        public ActionResult Index(int? index, int? size)
        {
			int idx = index.HasValue ? index.Value : 1;
			int sz = size.HasValue ? size.Value : 20;
			var result = _adminService.GetProducts(idx, sz);
            return View(result);
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

		[MyAuthorize]
		public ActionResult AddProduct(ProductRequest request)
		{
			if(ModelState.IsValid)
			{
				_adminService.AddProduct(request);
				return SuccessResult();
			}
			else
			{
				return FailedResult();	
			}
		}

		[MyAuthorize]
		public ActionResult ModifyProduct(ProductRequest request)
		{
			if (ModelState.IsValid)
			{
				_adminService.ModifyProduct(request);
				return SuccessResult();
			}
			else
			{
				return FailedResult();	
			}
		}

		[MyAuthorize]
		public ActionResult DeleteProduct(ProductRequest request)
		{
			_adminService.DeleteProduct(request);
			return SuccessResult();
		}

		private ActionResult SuccessResult()
		{
			return Json(new MessageBase(0, "成功"));
		}

		private ActionResult FailedResult()
		{
			StringBuilder sb = new StringBuilder();
			if (ModelState.Values != null)
			{
				foreach (var ms in ModelState.Values)
				{
					if (ms.Errors != null)
					{
						var modelError = ms.Errors.FirstOrDefault();
						if (modelError != null)
						{
							sb.Append(modelError.ErrorMessage ?? "");
							sb.Append(";");
						}
					}
				}
			}
			return Json(new MessageBase((int)ErrorCode.RequestInvalid, sb.ToString()));
		}
    }
}