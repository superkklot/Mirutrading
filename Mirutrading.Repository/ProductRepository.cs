﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using Mirutrading.Repository.Interfaces;
using Mirutrading.Repository.Models;
using Mirutrading.Infrastructure;
using Mirutrading.Repository.ValueObjects;

namespace Mirutrading.Repository
{
	public class ProductRepository : IProductRepository
	{
		private const string _productCollection = "Products";
		private IMongoCollection<BsonDocument> _collection;

		public ProductRepository()
		{
			_collection = MongoClientSingleton.Instance().Database.GetCollection<BsonDocument>(_productCollection);
		}

		public void Add(Product prd)
		{
			var now = DateTime.Now;
			prd.Status = (int)PrdStatus.active;
			prd.CreateDate = now;
			prd.UpdateDate = now;
			var bsonDoc = prd.ToBson();
			var task = _collection.InsertOneAsync(bsonDoc);
			task.Wait();
		}

		public void Modify(Product prd)
		{
			if (string.IsNullOrWhiteSpace(prd._id)) return;
			var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(prd._id));
			var update = Builders<BsonDocument>.Update.Set("Type", prd.Type)
				.Set("Name", prd.Name).Set("Price", prd.Price).Set("LinkUrl", prd.LinkUrl).CurrentDate("UpdateDate");
			_collection.UpdateOneAsync(filter, update).Wait();
		}

		public void Delete(Product prd)
		{
			if (string.IsNullOrWhiteSpace(prd._id)) return;
			var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(prd._id));
			var update = Builders<BsonDocument>.Update.Set("Status", 1);
			_collection.UpdateOneAsync(filter, update).Wait();
		}

		public List<Product> FindAll()
		{
			List<Product> result = new List<Product>();
			var builder = Builders<BsonDocument>.Filter;
			var filter = builder.Eq("Status", 0) | builder.Exists("Status", false);
			var sort = Builders<BsonDocument>.Sort.Descending("UpdateDate");
			_collection.Find(filter).Sort(sort).ToListAsync().ContinueWith(t =>
			{
				var list = t.Result;
				foreach(var item in list)
				{
					result.Add(item.ToProduct());
				}
			}).Wait();
			return result;

			//List<Product> result = new List<Product>();
			//var filter = new BsonDocument();
			//var count = 0;
			//_collection.FindAsync(filter).ContinueWith(t =>
			//{
			//	using (var cursor = t.Result)
			//	{
			//		bool ret = true;
			//		while (ret)
			//		{
			//			var task = cursor.MoveNextAsync().ContinueWith<bool>(tt =>
			//			{
			//				if (tt.Result == true)
			//				{
			//					var batch = cursor.Current;
			//					foreach (var document in batch)
			//					{
			//						var prd = document.ToProduct();
			//						result.Add(prd);
			//						count++;
			//					}
			//				}
			//				return tt.Result;
			//			});
			//			task.Wait();
			//			ret = task.Result;
			//		}
			//	}
			//}).Wait();
			//return result;
		}

		//目前看iis7.5 上跑有问题
		private async Task<List<Product>> _FindAll()
		{
			List<Product> result = new List<Product>();
			var filter = new BsonDocument();
			var count = 0;
			using (var cursor = await _collection.FindAsync(filter))
			{
				while (await cursor.MoveNextAsync())
				{
					var batch = cursor.Current;
					foreach (var document in batch)
					{
						var prd = document.ToProduct();
						result.Add(prd);
						count++;
					}
				}
			}
			return result;
		}

		public PagedCollection<Product> Get(int pageindex, int pagesize)
		{
			var fullList = FindAll();
			List<Product> prds = fullList.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
			return new PagedCollection<Product>(prds, pageindex, pagesize, fullList.Count);
		}
	}
}
