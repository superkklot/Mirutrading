using Mirutrading.Application.Interface;
using Mirutrading.Infrastructure.ExceptionHandling;
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
        private const int _defaultPrdPageCount = 5;
        private const int _defaultSearchPageCount = 10;
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

        public ActionResult GetPageBra(int? index)
        {
            int idx = index.HasValue ? index.Value : 1;
            var pagedProducts = _homeService.GetBraProducts(idx, _defaultPrdPageCount,
                _imgService.AvailableImages()[0]);
            pagedProducts.Items.ForEach(m => m.Image.Url = ConvertPath.Map(m.Image.Url));
            return Json(pagedProducts, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPageBrief(int? index)
        {
            int idx = index.HasValue ? index.Value : 1;
            var pagedProducts = _homeService.GetBriefsProducts(idx, _defaultPrdPageCount,
                _imgService.AvailableImages()[0]);
            pagedProducts.Items.ForEach(m => m.Image.Url = ConvertPath.Map(m.Image.Url));
            return Json(pagedProducts, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string term, int? index)
        {
            var path = Server.MapPath(AdminController._search_index_path);
            int idx = index.HasValue ? index.Value : 1;
            var prds = _homeService.SearchProducts(idx, _defaultSearchPageCount, path, term);
            if(prds.Items.HasValue())
            {
                prds.Items.ForEach(m => m.Image.Url = ConvertPath.Map(m.Image.Url));
            }
            return View(prds);
        }

        public ActionResult About()
        {
            //ViewBag.Message = @"";
            return View();
            //try
            //{
            //    ViewBag.Message = "1";

            //    if(ViewBag.Message="1")
            //        throw new BussinessException<OneErrorCode>(OneErrorCode.OneError);

            //    return View();
            //}
            //catch (BusinessException be)
            //{
            //    return View(be.Message);
            //}
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";
            
            return View();
        }
    }
}