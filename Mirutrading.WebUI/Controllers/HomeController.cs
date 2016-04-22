using Mirutrading.Application.Interface;
using Mirutrading.WebUI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Mirutrading.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private const int _defaultPrdPageCount = 12;
        private IHomeService _homeService;
        private IImageService _imgService;
        public HomeController(IHomeService homeService
            , IImageService imgService)
        {
            _homeService = homeService;
            _imgService = imgService;
        }

        public ActionResult Index()
        {
            var pagedProducts = _homeService.GetBraProducts(1, _defaultPrdPageCount,
                _imgService.AvailableImages()[0]);
            pagedProducts.Items.ForEach(m => m.Image.Url = ConvertPath.Map(m.Image.Url));
            return View(pagedProducts);
        }

        public ActionResult GetFirstPageBra()
        {
            var pagedProducts = _homeService.GetBraProducts(1, _defaultPrdPageCount,
                _imgService.AvailableImages()[0]);
            pagedProducts.Items.ForEach(m => m.Image.Url = ConvertPath.Map(m.Image.Url));
            return Json(pagedProducts, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFirstPageBrief()
        {
            var pagedProducts = _homeService.GetBriefsProducts(1, _defaultPrdPageCount,
                _imgService.AvailableImages()[0]);
            pagedProducts.Items.ForEach(m => m.Image.Url = ConvertPath.Map(m.Image.Url));
            return Json(pagedProducts, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
			
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            
            return View();
        }
    }
}