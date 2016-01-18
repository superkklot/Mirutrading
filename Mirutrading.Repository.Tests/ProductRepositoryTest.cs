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
				Type = 1,
				Name = "Hello",
				Price = 10,
				LinkUrl = "http://www.baidu.com"
			};
			ProductRepository repository = new ProductRepository();
			repository.Add(prd);
		}

		[TestMethod]
		public void FindAllProducts()
		{
			ProductRepository repository = new ProductRepository();
			var ret = repository.FindAll();
		}
	}
}
