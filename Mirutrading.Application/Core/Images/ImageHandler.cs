using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Application.Core.Images
{
	public class ImageHandler
	{
		private static readonly List<ImageSize> _imgsizes;
		private IImageProvider _imgProvider;

		static ImageHandler()
		{
			_imgsizes = new List<ImageSize>();
			_imgsizes.Add(new ImageSize() { Height = 128, Width = 228 });
			_imgsizes.Add(new ImageSize() { Height = 280, Width = 500 });
		}

		public static List<ImageSize> GetImgSizes()
		{
			return _imgsizes;
		}

		public ImageHandler()
		{
			_imgProvider = new ScaleCutProvider();
		}

		public List<string> CutImg(string path)
		{
			// load file
			List<string> imgPaths = new List<string>();
			HandledImageRequest request = new HandledImageRequest();
			foreach (var imgsize in _imgsizes)
			{
				request.ImgSize = imgsize;
				var ret = _imgProvider.Cut(request);
				if(ret != null && ret.Img != null && !string.IsNullOrWhiteSpace(ret.Name))
				{
					// store file
					imgPaths.Add(ret.Name);
				}
			}
			return imgPaths;
		}
	}
}
