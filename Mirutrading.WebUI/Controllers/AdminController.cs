using Mirutrading.WebUI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mirutrading.Application;
using Mirutrading.Application.ViewModel.Admin;

namespace Mirutrading.WebUI.Controllers
{
    [MyAuthorize]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginRequest request)
        {
            return View();
        }
    }
}