using EmitMapper;
using Mirutrading.Application.Core.Images;
using Mirutrading.Application.Interface;
using Mirutrading.Application.ViewModel.Admin;
using Mirutrading.Repository.Interfaces;
using Mirutrading.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Application.Service
{
	public class ImageService : IImageService
	{
		private IImageRepository _imgRepository;
		private static ObjectsMapper<Image, ImageRequest> _imageMapper_ptov;

		static ImageService()
		{
			_imageMapper_ptov = ObjectMapperManager.DefaultInstance.GetMapper<Image, ImageRequest>();
		}

		public ImageService(IImageRepository imgRepository)
		{
			_imgRepository = imgRepository;
		}

		public List<ImageRequest> GetImages(string productId)
		{
			var imgs = _imgRepository.GetByProductId(productId);
			List<ImageRequest> imgRequests = new List<ImageRequest>();
			foreach(var img in imgs)
			{
				var imgRequest = _imageMapper_ptov.Map(img);
				imgRequests.Add(imgRequest);
			}
			return imgRequests;
		}

		public void SaveFile(string productid, string vpath, string ppath)
		{
			ImageHandler imgHandler = new ImageHandler();
			var scaleFiles = imgHandler.CutImg(vpath, ppath);
			if (!scaleFiles.HasValue())
				throw new Exception("上传失败！");
			scaleFiles.Add(new HandledImageResponse()
			{
				ImgSize = new ImageSize() { Height = 0, Width = 0 },
				ImgUrl = vpath
			});
			foreach(var file in scaleFiles)
			{
				Image img = new Image()
				{
					ProductId = productid,
					Height = file.ImgSize.Height,
					Width = file.ImgSize.Width,
					Url = file.ImgUrl
				};
				_imgRepository.Add(img);
			}
		}
	}
}
