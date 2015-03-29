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
		private List<Product> Products { get; set; }

		public void Initialize()
			{
			}
		public Product GetProductById(int id)
			{
			return Products.Where(p => p.Id == id).Single();
			}
		}
	}
