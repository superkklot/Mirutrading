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
		private IMongoClient _client;
		private IMongoDatabase _database;
		private const string _dbname = "Mirutrading";
		private static MongoClientSingleton _instance;
		private static object _syncObjc = new object();

		protected MongoClientSingleton()
		{
			_client = new MongoClient(_conn);
			_database = _client.GetDatabase(_dbname);
		}

		public IMongoDatabase Database
		{
			get { return _database; }
		}

		public static MongoClientSingleton Instance()
		{
			if(_instance == null)
			{
				lock (_syncObjc)
				{
					if (_instance == null)
					{
						_instance = new MongoClientSingleton();
					}
				}
			}
			return _instance;
		}
	}
}
