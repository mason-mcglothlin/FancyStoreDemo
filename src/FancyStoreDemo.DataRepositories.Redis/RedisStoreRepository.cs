using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FancyStoreDemo.DataRepositories.Common;
using FancyStoreDemo.Models;
using Newtonsoft.Json;
using ServiceStack.Redis;

namespace FancyStoreDemo.DataRepositories.Redis
	{
	public class RedisStoreRepository : IStoreRepository
		{
		private RedisManagerPool redis { get; set; }

		public RedisStoreRepository(string connectionString)
			{
			redis = new RedisManagerPool(connectionString);
			}

		public void Initialize()
			{
			throw new NotImplementedException();
			}

		public Product GetProductById(int id)
			{
			using (var client = redis.GetClient())
				{
				var productsClient = client.As<Product>();
				return productsClient.GetById(id);
				}
			}

		public List<Product> GetAllProducts()
			{
			using (var client = redis.GetClient())
				{
				var productsClient = client.As<Product>();
				return productsClient.GetAll().ToList();
				}
			}

		public void AddNewProduct(Product product)
			{
			using (var client = redis.GetClient())
				{
				var productsClient = client.As<Product>();
				productsClient.Store(product);
				}
			}

		public void UpdateProduct(Product product)
			{
			using (var client = redis.GetClient())
				{
				var productsClient = client.As<Product>();
				productsClient.Store(product);
				}
			}

		public void DeleteProduct(int id)
			{
			throw new NotImplementedException();
			}
		}
	}
