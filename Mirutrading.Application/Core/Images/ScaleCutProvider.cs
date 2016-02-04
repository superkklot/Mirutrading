using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Mirutrading.Application.Core.Images
{
	class ScaleCutProvider : IImageProvider
	{
		public HandledImage Cut(HandledImageRequest request)
		{
			Image initImage = Image.FromStream(request.ImageStream, true);
			if (initImage.Width <= request.ImgSize.Width
				&& initImage.Height <= request.ImgSize.Height)
			{
				return new HandledImage()
				{
					Img = initImage,
					Name = request.SrcName
				};
			}

			double templateRate = (double)request.ImgSize.Width / (double)request.ImgSize.Height;
			double initRate = (double)initImage.Width / (double)initImage.Height;

			if(templateRate == initRate)
			{
				// 等比例缩放
				return ScaleCut(request, initImage);
			}
			else if(templateRate > initRate)
			{
				return HeightCut(request, initImage, templateRate);
			}
			else
			{
				return WidthCut(request, initImage, templateRate);
			}
		}

		private HandledImage ScaleCut(HandledImageRequest request, Image initImage)
		{
			Image templateImg = new Bitmap(request.ImgSize.Width, request.ImgSize.Height);
			Graphics templateGph = Graphics.FromImage(templateImg);
			templateGph.InterpolationMode = InterpolationMode.High;
			templateGph.SmoothingMode = SmoothingMode.HighQuality;
			templateGph.Clear(Color.White);
			templateGph.DrawImage(initImage, new Rectangle(0, 0, request.ImgSize.Width, request.ImgSize.Height),
				new Rectangle(0, 0, initImage.Width, initImage.Height), GraphicsUnit.Pixel);
			templateGph.Dispose();
			return new HandledImage()
			{
				Img = templateImg,
				Name = string.Format(request.SrcName + "_{0}_{1}_{2}", request.ImgSize.Width, request.ImgSize.Height, request.Quality);
			};
		}

		private HandledImage WidthCut(HandledImageRequest request, Image initImage, double templateRate)
		{
			int newWidth = (int)Math.Floor(initImage.Height * templateRate);
			Image pickedImg = new Bitmap(newWidth, initImage.Height);
			Graphics pickedGph = Graphics.FromImage(pickedImg);
			Rectangle fromR = new Rectangle();
			fromR.X = (int)Math.Floor((double)(initImage.Width - newWidth)/2); fromR.Y = 0;
			fromR.Width = newWidth; fromR.Height = initImage.Height;

			Rectangle toR = new Rectangle();
			toR.X = 0; toR.Y = 0;
			toR.Width = newWidth; toR.Height = initImage.Height;
			// 裁减
			pickedGph.DrawImage(initImage, toR, fromR, GraphicsUnit.Pixel);
			pickedGph.Dispose();
			return ScaleCut(request, pickedImg);
		}

		private HandledImage HeightCut(HandledImageRequest request,Image initImage, double templateRate)
		{
			int newHeight = (int)Math.Floor(initImage.Width / templateRate);
			Image pickedImg = new Bitmap(initImage.Width, newHeight);
			Graphics pickedGph = Graphics.FromImage(pickedImg);
			Rectangle fromR = new Rectangle();
			fromR.X = 0; fromR.Y = (int)Math.Floor((double)(initImage.Height - newHeight) / 2);
			fromR.Width = initImage.Width; fromR.Height = newHeight;

			Rectangle toR = new Rectangle();
			toR.X = 0; toR.Y = 0;
			toR.Width = initImage.Width; toR.Height = newHeight;
			// 裁减
			pickedGph.DrawImage(initImage, toR, fromR, GraphicsUnit.Pixel);
			pickedGph.Dispose();
			return ScaleCut(request, pickedImg);
		}
	}
}
