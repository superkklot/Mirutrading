using Mirutrading.Application.Core.Models.Images;
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
    }
}
