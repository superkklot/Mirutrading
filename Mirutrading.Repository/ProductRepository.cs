using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using Mirutrading.Repository.Interfaces;
using Mirutrading.Repository.Models;

namespace Mirutrading.Repository
{
	public class ProductRepository : IProductRepository
	{
		private const string _productCollection = "Products";
		private IMongoCollection<BsonDocument> _collection;

		public ProductRepository()
		{
			_collection = MongoClientSingleton.Database.GetCollection<BsonDocument>(_productCollection); ;
		}

		public void Add(Product prd)
		{
			var bsonDoc = prd.ToBson();
			var task = _collection.InsertOneAsync(bsonDoc);
			task.Wait();
		}

		public List<Product> FindAll()
		{
			var task = _FindAll();
			task.Wait();
			return task.Result;
		}

		private async Task<List<Product>> _FindAll()
		{
			List<Product> result = new List<Product>();
			var filter = new BsonDocument();
			var count = 0;
			using(var cursor = await _collection.FindAsync(filter))
			{
				while(await cursor.MoveNextAsync())
				{
					var batch = cursor.Current;
					foreach(var document in batch)
					{
						var prd = document.ToProduct();
						result.Add(prd);
						count++;
					}
				}
			}
			return result;
		}
	}
}
