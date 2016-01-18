using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Repository.Models
{
	public class Product
	{
		public string _id { get; set; }
		public int Type { get; set; }
		public string Name { get; set; }
		public int Price { get; set; }
		public string LinkUrl { get; set; }
	}
}
