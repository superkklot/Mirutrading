using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace Mirutrading.Application.Core.Images
{
	public class ImageSize
	{
		public int Height { get; set; }
		public int Width { get; set; }
	}

	public class HandledImageRequest
	{
		
		public ImageSize ImgSize { get; set; }
		public Stream ImageStream { get; set; }
		public string SrcName { get; set; }
		public int Quality { get; set; }
	}

	public class HandledImageResponse
	{
		public ImageSize ImgSize { get; set; }
		public string ImgUrl { get; set; }
	}

	public class HandledImage
	{
		public string Name { get; set; }
		public Image Img { get; set; }
	}
}
