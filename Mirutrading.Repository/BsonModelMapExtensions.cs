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
		public static BsonDocument ToBson(this Image img)
		{
			var bsonDoc = new BsonDocument();
			bsonDoc.Add("ProductId", img.ProductId);
			bsonDoc.Add("Height", img.Height);
			bsonDoc.Add("Width", img.Width);
			bsonDoc.Add("Url", img.Url);
			bsonDoc.Add("Status", img.Status);
			bsonDoc.Add("CreateDate", img.CreateDate);
			bsonDoc.Add("UpdateDate", img.UpdateDate);
			return bsonDoc;
		}

		public static Image ToImage(this BsonDocument doc)
		{
			Image img = new Image();
			img._id = doc.GetValue("_id").AsObjectId.ToString();
			img.ProductId = doc.GetValue("ProductId").AsString;
			img.Height = doc.GetValue("Height").AsInt32;
			img.Width = doc.GetValue("Width").AsInt32;
			img.Url = doc.GetValue("Url").AsString;
			img.Status = doc.GetValue("Status").AsInt32;
			img.CreateDate = doc.GetValue("CreateDate").ToUniversalTime();
			img.UpdateDate = doc.GetValue("UpdateDate").ToUniversalTime();
			return img;
		}

		public static BsonDocument ToBson(this Product prd)
		{
			var bsonDoc = new BsonDocument();
			bsonDoc.Add("Type", prd.Type);
			bsonDoc.Add("Name", prd.Name);
			bsonDoc.Add("Price", prd.Price);
			bsonDoc.Add("LinkUrl", prd.LinkUrl);
			bsonDoc.Add("Status", prd.Status);
			bsonDoc.Add("CreateDate", prd.CreateDate);
			bsonDoc.Add("UpdateDate", prd.UpdateDate);
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
			prd.Status = doc.ExtTryGetValue<int>("Status").AsInt32;
			prd.CreateDate = doc.ExtTryGetValue<DateTime>("CreateDate").ToUniversalTime();
			prd.CreateDate = doc.ExtTryGetValue<DateTime>("UpdateDate").ToUniversalTime();
			return prd;
		}

		public static BsonValue ExtTryGetValue<T>(this BsonDocument doc, string name)
		{
			T t = default(T);
			BsonValue ret;
			bool boolret = doc.TryGetValue(name, out ret);
			if (!boolret) ret = BsonValue.Create(t);
			return ret;
		}
	}
}
