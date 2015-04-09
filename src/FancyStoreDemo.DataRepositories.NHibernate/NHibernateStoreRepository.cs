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
		private ISessionFactory sessionFactory { get; set; }
		public NHibernateStoreRepository(string sqliteFileName)
			{
			SessionFactory.CreateSessionFactory(sqliteFileName);
			}

		public void Initialize() { throw new NotImplementedException(); }

		public Product GetProductById(int id)
			{
			using (var session = sessionFactory.OpenSession())
				{
				var product = session.Get<Product>(id);
				return product;
				}

			}

		public List<Product> GetAllProducts()
			{
			using (var session = sessionFactory.OpenSession())
				{
				return session.CreateCriteria<Product>().List<Product>().ToList();
				}
			}

		public void AddNewProduct(Product product)
			{
			using (var session = sessionFactory.OpenSession())
				{
				using (var transaction = session.BeginTransaction())
					{
					session.Save(product, product.Id);
					transaction.Commit();
					}
				}
			}

		public void UpdateProduct(Product product)
			{
			using (var session = sessionFactory.OpenSession())
				{
				using (var transaction = session.BeginTransaction())
					{
					session.Update(product, product.Id);
					transaction.Commit();
					}
				}
			}

		public void DeleteProduct(int id)
			{
			using (var session = sessionFactory.OpenSession())
				{
				using (var transaction = session.BeginTransaction())
					{
					var queryString = String.Format("delete {0} where id = :id", typeof(Product));
					session.CreateQuery(queryString)
						   .SetParameter("id", id)
						   .ExecuteUpdate();

					transaction.Commit();
					}
				}
			}
		}
	}
