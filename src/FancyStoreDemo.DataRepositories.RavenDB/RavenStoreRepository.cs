using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FancyStoreDemo.DataRepositories.Common;
using FancyStoreDemo.Models;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Client.Indexes;

namespace FancyStoreDemo.DataRepositories.RavenDB
	{
	public class RavenStoreRepository : IStoreRepository
		{
		private IDocumentStore DB { get; set; }
		public RavenStoreRepository(string dataDirectory)
			{
			DB = new EmbeddableDocumentStore() { DataDirectory = dataDirectory };
			DB.Initialize();
			}
		public void Initialize()
			{

			}

		public Product GetProductById(int id)
			{
			using (var session = DB.OpenSession())
				{
				return session.Load<Product>(id);
				}
			}
		public List<Product> GetAllProducts()
			{
			using (var session = DB.OpenSession())
				{
				return session.Query<Product>().ToList();
				}
			}
		public void AddNewProduct(Product product)
			{
			using (var session = DB.OpenSession())
				{
				session.Store(product, "products/" + product.Id);
				session.SaveChanges();
				}
			}
		public void UpdateProduct(Product product)
			{
			using (var session = DB.OpenSession())
				{
				session.Store(product, "products/" + product.Id);
				session.SaveChanges();
				}
			}
		public void DeleteProduct(int id)
			{
			using (var session = DB.OpenSession())
				{
				session.Delete("products/" + id);
				session.SaveChanges();
				}
			}
		}
	}
