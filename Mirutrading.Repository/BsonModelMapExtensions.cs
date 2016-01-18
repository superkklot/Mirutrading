using Mirutrading.Repository.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Repository
{
	public static class BsonModelMapExtensions
	{
		public static BsonDocument ToBson(this Product prd)
		{
			var bsonDoc = new BsonDocument();
			bsonDoc.Add("Type", prd.Type);
			bsonDoc.Add("Name", prd.Name);
			bsonDoc.Add("Price", prd.Price);
			bsonDoc.Add("LinkUrl", prd.LinkUrl);
			return bsonDoc;
		}

		public static Product ToProduct(this BsonDocument doc)
		{
			Product prd = new Product();
			prd._id = doc.GetValue("_id").AsObjectId.ToString();
			prd.Type = doc.GetValue("Type").AsInt32;
			prd.Name = doc.GetValue("Name").AsString;
			prd.Price = doc.GetValue("Price").AsInt32;
			prd.LinkUrl = doc.GetValue("LinkUrl").AsString;
			return prd;
		}
	}
}
