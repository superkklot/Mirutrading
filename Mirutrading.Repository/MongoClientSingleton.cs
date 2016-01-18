using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Repository
{
	public class MongoClientSingleton
	{
		private static readonly string _conn = ConfigurationManager.ConnectionStrings["MongoConnection"].ConnectionString;
		private static readonly IMongoClient _client = new MongoClient(_conn);
		private static readonly IMongoDatabase _database = _client.GetDatabase(_dbname);
		public const string _dbname = "Mirutrading";

		public static IMongoDatabase Database
		{
			get { return _database; }
		}
	}
}
