using Mirutrading.Infrastructure;
using Mirutrading.Repository.Models;
using Mirutrading.Repository.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Repository.Interfaces
{
	public interface IProductRepository
	{
		void Add(Product prd);

		void Modify(Product prd);

		void Delete(Product prd);

		List<Product> FindAll();

		PagedCollection<Product> Get(int pageindex, int pagesize);

        PagedCollection<Product> GetByType(int pageindex, int pagesize, PrdType prdType);
	}
}
