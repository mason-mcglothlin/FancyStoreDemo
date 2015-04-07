using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FancyStoreDemo.DataRepositories.Common;
using FancyStoreDemo.Models;

namespace FancyStoreDemo.DataRepositories.EntityFramework
	{
	public class EntityFrameworkStoreRepository : IStoreRepository
		{
		private FancyStoreEFDBContext db;
		public EntityFrameworkStoreRepository(string connectionString)
			{
			db = new FancyStoreEFDBContext(connectionString);
			}
		public void Initialize() { }
		public Product GetProductById(int id)
			{
			return db.Products.Where(p => p.Id == id).Single();
			}

		public List<Product> GetAllProducts()
			{
			return db.Products.ToList();
			}

		public void AddNewProduct(Product product)
			{
			db.Products.Add(product);
			db.SaveChanges();
			}

		public void UpdateProduct(Product product)
			{
			db.Entry(product).State = EntityState.Modified;
			db.SaveChanges();
			}

		public void DeleteProduct(int id)
			{
			var product = GetProductById(id);
			db.Entry(product).State = EntityState.Deleted;
			db.SaveChanges();
			}
		}
	}
