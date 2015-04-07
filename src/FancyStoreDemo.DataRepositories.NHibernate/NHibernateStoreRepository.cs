using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FancyStoreDemo.DataRepositories.Common;
using FancyStoreDemo.Models;
using NHibernate;

namespace FancyStoreDemo.DataRepositories.NHibernate
	{
	public class NHibernateStoreRepository : IStoreRepository
		{
		public NHibernateStoreRepository()
		{ }

		public void Initialize() { throw new NotImplementedException(); }

		public Product GetProductById(int id)
			{
			var sessionFactory = SessionFactory.CreateSessionFactory();
			using (var session = sessionFactory.OpenSession())
				{
				using (var transaction = session.BeginTransaction())
					{
					var product = session.Get<Product>(id);
					return product;
					}
				}

			}

		public List<Product> GetAllProducts() { throw new NotImplementedException(); }

		public void AddNewProduct(Product product) { throw new NotImplementedException(); }

		public void UpdateProduct(Product product) { throw new NotImplementedException(); }

		public void DeleteProduct(int id) { throw new NotImplementedException(); }
		}
	}
