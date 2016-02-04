using Mirutrading.Repository.Interfaces;
using Mirutrading.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Repository
{
	public class ImageRepository : IImageRepository
	{
		public List<Image> GetByProductId(string productId)
		{
			throw new NotImplementedException();
		}

		public void Add(Image img)
		{
			throw new NotImplementedException();
		}

		public void Modify(Image img)
		{
			throw new NotImplementedException();
		}
	}
}
