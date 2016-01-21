using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Application.ViewModel.Admin
{
	public class ProductRequest
	{
		public string _id { get; set; }
		[Required]
		public int Type { get; set; }
		[Required]
		[StringLength(50)]
		public string Name { get; set; }
		public int Price { get; set; }
		[Required]
		[StringLength(200)]
		public string LinkUrl { get; set; }
	}
}
