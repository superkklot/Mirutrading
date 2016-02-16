using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Application.ViewModel.Admin
{
	public class ImageRequest
	{
		public string _id { get; set; }
		public string ProductId { get; set; }
		public int Height { get; set; }
		public int Width { get; set; }
		public string Url { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime UpdateDate { get; set; }
	}
}
