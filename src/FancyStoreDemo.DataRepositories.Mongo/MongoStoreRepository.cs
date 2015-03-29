using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FancyStoreDemo.DataRepositories.Common;
using FancyStoreDemo.Models;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;


namespace FancyStoreDemo.DataRepositories.Mongo
	{
	public class MongoStoreRepository : IStoreRepository
		{
		private string ConnectionString { get; set; }
		private const string DatabaseName = "FancyStore";

		private MongoDatabase GetDatabase()
			{
			var client = new MongoClient(ConnectionString);
			return client.GetServer().GetDatabase(DatabaseName);
			}

		private MongoCollection<Product> GetProductsCollection()
			{
			return GetDatabase().GetCollection<Product>("Products");
			}

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
			return GetProductsCollection().AsQueryable<Product>().Where(p => p.Id == id).Single();
			}

		public List<Product> GetAllProducts()
			{
			return GetProductsCollection().FindAll().ToList();
			}

		public void AddNewProduct(Product product)
			{
			GetProductsCollection().Insert(product);
			}

		public void UpdateProduct(Product product)
			{
			GetProductsCollection().Save(product);
			}

		public void DeleteProduct(int id)
			{
			GetProductsCollection().Remove(Query<Product>.EQ(p => p.Id, id));
			}
		}
	}
