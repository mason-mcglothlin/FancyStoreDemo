using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FancyStoreDemo.DataRepositories.Common;
using FancyStoreDemo.Models;

namespace FancyStoreDemo.DataRepositories.SqlLite
	{
	public class SqlLiteStoreRepository : IStoreRepository
		{
		private string ConnectionString { get; set; }

		public SqlLiteStoreRepository(string connectionString)
			{
			ConnectionString = connectionString;
			}

		public void Initialize()
			{
			throw new NotImplementedException();
			var command = new SQLiteCommand("create table products ()");
			SqlLiteDatabaseHelpers.ExecuteNonQuery(command, ConnectionString);
			}

		public Product GetProductById(int id)
			{
			var command = new SQLiteCommand("select id, name, description, price from products where id=:id");
			command.Parameters.AddWithValue("id", id);
			var dt = SqlLiteDatabaseHelpers.GetDataTable(command, ConnectionString);
			return dt.AsEnumerable().Select(r => new Product()
			{
				Id = r.Field<int>("id"),
				Name = r.Field<string>("name"),
				Description = r.Field<string>("description"),
				Price = r.Field<double>("price")
			}).Single();
			}
		}
	}
