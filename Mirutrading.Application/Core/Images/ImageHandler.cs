using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing.Imaging;
using Mirutrading.Application.Core.Models.Images;

namespace Mirutrading.Application.Core.Images
{
	public class ImageHandler
	{
		private static readonly List<ImageSize> _imgsizes;
		private IImageProvider _imgProvider;

		static ImageHandler()
		{
			_imgsizes = new List<ImageSize>();
			_imgsizes.Add(new ImageSize() { Height = 180, Width = 180 });
			_imgsizes.Add(new ImageSize() { Height = 120, Width = 120 });
		}

		public static List<ImageSize> GetImgSizes()
		{
			return _imgsizes;
		}

		public ImageHandler()
		{
			_imgProvider = new ScaleCutProvider();
		}

		public List<HandledImageResponse> CutImg(string vpath, string ppath)
		{
			// load file
			bool fileExists = File.Exists(ppath);
			if (!fileExists) return null;
			List<HandledImageResponse> imgPaths = new List<HandledImageResponse>();
			string pdirStr = GetDirectory(ppath);
			string vdirStr = GetVirtualDirectory(vpath);
			HandledImageRequest request = new HandledImageRequest();
			request.ImageStream = File.OpenRead(ppath);
			request.Quality = 100;
			request.SrcName = GetFileName(ppath);
			foreach (var imgsize in _imgsizes)
			{
				request.ImgSize = imgsize;
				var ret = _imgProvider.Cut(request);
				if(ret != null && ret.Img != null && !string.IsNullOrWhiteSpace(ret.Name))
				{
					// store file
					string scaleFilePath = string.Format("{0}\\{1}.jpg", pdirStr, ret.Name);
					ret.Img.Save(scaleFilePath, ImageFormat.Jpeg);
					ret.Img.Dispose();
					string scaleVisualFilePath = string.Format("{0}/{1}.jpg", vdirStr, ret.Name);
					imgPaths.Add(new HandledImageResponse() { ImgSize = imgsize, ImgUrl = scaleVisualFilePath });
				}
			}
			request.ImageStream.Close();
			return imgPaths;
		}

		private string GetDirectory(string path)
		{
			int index = path.LastIndexOf('\\');
			if (index == -1) return path;
			return path.Substring(0, index);
		}

		private string GetVirtualDirectory(string path)
		{
			int index = path.LastIndexOf('/');
			if (index == -1) return path;
			return path.Substring(0, index);
		}

		private string GetFileName(string path)
		{
			int index = path.LastIndexOf('\\');
			if (index == -1) return path;
			string fileName = path.Substring(index + 1);
			int index2 = fileName.IndexOf('.');
			if (index2 == -1) return fileName;
			return fileName.Substring(0, index2);
		}
	}
}
