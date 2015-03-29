using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FancyStoreDemo.DataRepositories.Common;
using FancyStoreDemo.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;


namespace FancyStoreDemo.DataRepositories.Mongo
	{
	public class MongoStoreRepository : IStoreRepository
		{
		private string ConnectionString { get; set; }

		public MongoStoreRepository(string connectionString)
			{
			ConnectionString = connectionString;
			}

		public void Initialize()
			{
			throw new NotImplementedException();
			}

		public Product GetProductById(int id)
			{
			var client = new MongoClient(ConnectionString);
			var db = client.GetServer().GetDatabase("FancyStore");
			return db.GetCollection<Product>("Products").AsQueryable<Product>().Where(p => p.Id == id).Single();
			}
		}
	}
