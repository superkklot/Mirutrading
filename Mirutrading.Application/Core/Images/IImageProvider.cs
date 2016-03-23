using Mirutrading.Application.Core.Models.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Application.Core.Images
{
	interface IImageProvider
	{
		HandledImage Cut(HandledImageRequest request);
	}
}
