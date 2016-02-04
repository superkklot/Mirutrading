using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mirutrading.WebUI.Controllers
{
	public class ImageController : Controller
	{
		public ViewResult Index(string productId)
		{
			return View();
		}
	}
}