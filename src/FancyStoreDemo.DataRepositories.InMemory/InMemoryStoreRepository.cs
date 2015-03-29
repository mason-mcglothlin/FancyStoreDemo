using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FancyStoreDemo.DataRepositories.Common;
using FancyStoreDemo.Models;

namespace FancyStoreDemo.DataRepositories.InMemory
	{
	public class InMemoryStoreRepository : IStoreRepository
		{
		private Dictionary<int, Product> Products { get; set; }

		public void Initialize()
			{
			}
		public Product GetProductById(int id)
			{
			return Products[id];
			}

		public void AddNewProduct(Product product)
			{
			Products.Add(product.Id, product);
			}

		public void UpdateProduct(Product product)
			{
			Products[product.Id] = product;
			}

		public void DeleteProduct(int id)
			{
			Products.Remove(id);
			}
		}
	}
