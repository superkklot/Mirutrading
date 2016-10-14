using Mirutrading.Application.Core.Models.Images;
using Mirutrading.Application.Core.SearchEngine;
using Mirutrading.Application.Core.Weixin;
using Mirutrading.Application.Interface;
using Mirutrading.Application.ViewModel.Admin;
using Mirutrading.Application.ViewModel.Home;
using Mirutrading.Infrastructure;
using Mirutrading.Repository.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Application.Service
{
    public class HomeService : IHomeService
    {
        private IAdminService _adminService;
        private IImageService _imgService;
        public HomeService(IAdminService adminService
            , IImageService imgService)
        {
            _adminService = adminService;
            _imgService = imgService;
        }

        private PagedCollection<IndexProduct> GetProducts(int pageindex, int pagesize, ImageSize imgSize, PrdType prdType)
        {
            var pagedProducts = _adminService.GetProductsByType(pageindex, pagesize, prdType);
            List<IndexProduct> indexProducts = new List<IndexProduct>();
            foreach (var item in pagedProducts.Items)
            {
                var imgs = _imgService.GetImages(item._id);
                var selectImg = imgs.FirstOrDefault(m => m.Width == imgSize.Width && m.Height == imgSize.Height);
                if (selectImg == null) continue;
                IndexProduct idxProduct = new IndexProduct();
                idxProduct.Product = item;
                idxProduct.Image = selectImg;
                indexProducts.Add(idxProduct);
            }
            return new PagedCollection<IndexProduct>(indexProducts, pagedProducts.PageIndex,
                pagedProducts.PageSize, pagedProducts.ItemCount);
        }

        public PagedCollection<IndexProduct> GetBraProducts(int pageindex, int pagesize, ImageSize imgSize)
        {
            return GetProducts(pageindex, pagesize, imgSize, PrdType.Bra);
        }

        public PagedCollection<IndexProduct> GetBriefsProducts(int pageindex, int pagesize, ImageSize imgSize)
        {
            return GetProducts(pageindex, pagesize, imgSize, PrdType.Briefs);
        }

        public PagedCollection<IndexProduct> SearchProducts(int pageindex, int pagesize, string path, string term)
        {
            var search = SearchProvider.GetSearch();
            var searchProducts = search.SearchProducts(path, term);
            var pagedProducts = searchProducts.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
            return new PagedCollection<IndexProduct>(pagedProducts, pageindex, pagesize, searchProducts.Count);
        }

        public WeixinPayInfo GetPayInfo(string ip)
        {
            WeixinPayInfo payInfo = new WeixinPayInfo();
            payInfo.AppId = "xxx";
            payInfo.NonceStr = WeixinPayUtil.GetNoncestr();
            payInfo.TimeStamp = WeixinPayUtil.GetTimestamp();
            payInfo.Package = "prepay_id=" + GetPrePayId(ip);
            payInfo.PaySign = GetPaySign(payInfo, "paySignKey");
            return payInfo;
        }

        private string GetPaySign(WeixinPayInfo payInfo, string paySignKey)
        {
            SortedDictionary<string, string> sParams = new SortedDictionary<string, string>();
            sParams.Add("appId", payInfo.AppId);
            sParams.Add("timeStamp", payInfo.TimeStamp);
            sParams.Add("nonceStr", payInfo.NonceStr);
            sParams.Add("package", payInfo.Package);
            sParams.Add("signType", "MD5");
            return WeixinPayUtil.GetSign(sParams, paySignKey);
        }

        private string GetPrePayId(string ip)
        {
            UnifiedOrder order = new UnifiedOrder();
            order.appid = "xxx";
            order.attach = "version";
            order.body = 10 + "人民币";
            order.device_info = "";
            order.mch_id = "mch_id";
            order.nonce_str = WeixinPayUtil.GetNoncestr();
            order.notify_url = "http://localhost/sample/a.aspx";
            order.openid = "openid";
            order.out_trade_no = "order_id";
            order.trade_type = "JSAPI";
            order.spbill_create_ip = ip;
            order.total_fee = 10 * 100;

            return WeixinPayUtil.GetPrepayId(order, "paySignKey");
        }
    }
}
