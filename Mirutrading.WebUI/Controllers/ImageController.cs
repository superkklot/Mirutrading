using Mirutrading.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mirutrading.WebUI.Controllers
{
	public class ImageController : Controller
	{
		private IImageService _imageService;
		public ImageController(IImageService imageService)
		{
			_imageService = imageService;
		}

		public ViewResult Index(string productId)
		{
			var imgModel = _imageService.GetImages(productId);
			ViewBag.Pid = productId;
			return View(imgModel);
		}

		public ViewResult Upload(string productId, HttpPostedFileBase file)
		{
			try
			{
				string saveFile = SaveFile(file);
				_imageService.SaveFile(productId, saveFile, Server.MapPath(saveFile));
				ViewBag.Msg = "上传成功！";
				ViewBag.Pid = productId;
				return View();
			}
			catch(Exception ex)
			{
				ViewBag.Msg = ex.Message;
				return View();
			}
		}

		private string SaveFile(HttpPostedFileBase file)
		{
			if (file == null)
				throw new Exception("请指定上传的文件！");
			var fileName = file.FileName.ToLower();
			if (!fileName.EndsWith(".jpg") && !fileName.EndsWith(".gif"))
				throw new Exception("请上传jpg、gif格式的文件！");
			if (file.InputStream.Length > 2097152)
				throw new Exception("上传的文件必须小于2M！");
			var saveFile = Guid.NewGuid().ToString();
			string saveFilePath = string.Format("~/Images/{0}.jpg", saveFile);
			string physicalPath = Server.MapPath(saveFilePath);
			file.SaveAs(physicalPath);
			return saveFilePath;
		}
	}
}