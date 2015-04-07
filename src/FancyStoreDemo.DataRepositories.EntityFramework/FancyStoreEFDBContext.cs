using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FancyStoreDemo.Models;

namespace FancyStoreDemo.DataRepositories.EntityFramework
	{
	class FancyStoreEFDBContext : DbContext
		{
		public FancyStoreEFDBContext(string connectionString)
			: base(connectionString)
			{
			}
		public DbSet<Product> Products { get; set; }
		}
	}
