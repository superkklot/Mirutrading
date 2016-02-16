using Mirutrading.Repository.Interfaces;
using Mirutrading.Repository.Models;
using Mirutrading.Repository.ValueObjects;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Repository
{
	public class ImageRepository : IImageRepository
	{
		private const string _imageCollection = "Images";
		private IMongoCollection<BsonDocument> _collection;

		public ImageRepository()
		{
			_collection = MongoClientSingleton.Instance().Database.GetCollection<BsonDocument>(_imageCollection);
		}

		public List<Image> GetByProductId(string productId)
		{
			List<Image> result = new List<Image>();
			var builder = Builders<BsonDocument>.Filter;
			var filter = builder.Eq("Status", 0) & builder.Eq("ProductId", productId);
			var sort = Builders<BsonDocument>.Sort.Descending("UpdateDate");
			_collection.Find(filter).Sort(sort).ToListAsync().ContinueWith(t =>
			{
				var list = t.Result;
				foreach (var item in list)
				{
					result.Add(item.ToImage());
				}
			}).Wait();
			return result;
		}

		public void Add(Image img)
		{
			var now = DateTime.Now;
			img.Status = (int)ImgStatus.active;
			img.CreateDate = now;
			img.UpdateDate = now;
			var bsonDoc = img.ToBson();
			var task = _collection.InsertOneAsync(bsonDoc);
			task.Wait();
		}

		public void Modify(Image img)
		{
			throw new NotImplementedException();
		}
	}
}
