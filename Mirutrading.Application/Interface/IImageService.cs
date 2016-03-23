using Mirutrading.Application.Core.Models.Images;
using Mirutrading.Application.ViewModel.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Application.Interface
{
	public interface IImageService
	{
		List<ImageRequest> GetImages(string productId);
        List<ImageSize> AvailableImages();
		void SaveFile(string productid, string vpath, string ppath);
	}
}
