using Mirutrading.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Repository.Interfaces
{
	public interface IImageRepository
	{
		List<Image> GetByProductId(string productId);

		void Add(Image img);

		void Modify(Image img);
	}
}
