using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mirutrading.Repository.Models;

namespace Mirutrading.Repository.Tests
{
	[TestClass]
	public class ProductRepositoryTest
	{
		//[TestMethod]
		public void AddProduct()
		{
			// Arrange
			Product prd = new Product()
			{
				Type = 3,
				Name = "ccc",
				Price = 11,
				LinkUrl = "http://www.bb.com"
			};
			ProductRepository repository = new ProductRepository();
			repository.Add(prd);
		}

		//[TestMethod]
		public void FindAllProducts()
		{
			ProductRepository repository = new ProductRepository();
			var ret = repository.FindAll();
			repository = new ProductRepository();
			ret = repository.FindAll();
		}
	}
}
