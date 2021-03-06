﻿using EmitMapper;
using Mirutrading.Application.Common;
using Mirutrading.Application.Core.SearchEngine;
using Mirutrading.Application.Interface;
using Mirutrading.Application.ViewModel.Admin;
using Mirutrading.Infrastructure;
using Mirutrading.Repository.Interfaces;
using Mirutrading.Repository.Models;
using Mirutrading.Repository.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Application.Service
{
	public class AdminService : IAdminService
	{
		private IProductRepository _productRepository;
		private static ObjectsMapper<ProductRequest, Product> _productMapper_vtop;
		private static ObjectsMapper<Product, ProductRequest> _productMapper_ptov;

		static AdminService()
		{
			_productMapper_vtop = ObjectMapperManager.DefaultInstance.GetMapper<ProductRequest, Product>();
			_productMapper_ptov = ObjectMapperManager.DefaultInstance.GetMapper<Product, ProductRequest>();
		}

		public AdminService(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public bool IsUserAuthorized(LoginRequest request)
		{
			var targetToken = AppConfigHelper.AdminToken;
			var source = string.Format("{0}_{1}", request.UserName, request.Password);
			var sourceToken = Security.ComputeMd5String(source);
			return targetToken == sourceToken;
		}

		public void AddProduct(ProductRequest request)
		{
			Product prd = _productMapper_vtop.Map(request);
			_productRepository.Add(prd);
		}

		public void ModifyProduct(ProductRequest request)
		{
			Product prd = _productMapper_vtop.Map(request);
			_productRepository.Modify(prd);
		}

		public PagedCollection<ProductRequest> GetProducts(int pageindex, int pagesize)
		{
			var prds = _productRepository.Get(pageindex, pagesize);
			List<ProductRequest> prdRequests = new List<ProductRequest>();
			foreach (var prd in prds.Items)
			{
				ProductRequest prdReqeust = _productMapper_ptov.Map(prd);
				prdRequests.Add(prdReqeust);
			}
			return new PagedCollection<ProductRequest>(prdRequests, prds.PageIndex, prds.PageSize, prds.ItemCount);
		}

        public PagedCollection<ProductRequest> GetProductsByType(int pageindex, int pagesize, PrdType prdType)
        {
            var prds = _productRepository.GetByType(pageindex, pagesize, prdType);
            List<ProductRequest> prdRequests = new List<ProductRequest>();
            foreach (var prd in prds.Items)
            {
                ProductRequest prdReqeust = _productMapper_ptov.Map(prd);
                prdRequests.Add(prdReqeust);
            }
            return new PagedCollection<ProductRequest>(prdRequests, prds.PageIndex, prds.PageSize, prds.ItemCount);
        }

		public void DeleteProduct(ProductRequest request)
		{
			Product prd = _productMapper_vtop.Map(request);
			_productRepository.Delete(prd);
		}

        public void CreateIndex(string path)
        {
            ISearch search = SearchProvider.GetSearch();
            search.CreateIndexForProducts(path);
        }
	}
}
