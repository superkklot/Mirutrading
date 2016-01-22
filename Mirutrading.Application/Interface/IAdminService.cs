using Mirutrading.Application.ViewModel.Admin;
using Mirutrading.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Application.Interface
{
	public interface IAdminService
	{
		bool IsUserAuthorized(LoginRequest request);

		void AddProduct(ProductRequest request);

		void ModifyProduct(ProductRequest request);

		PagedCollection<ProductRequest> GetProducts(int pageindex, int pagesize);

	}
}
