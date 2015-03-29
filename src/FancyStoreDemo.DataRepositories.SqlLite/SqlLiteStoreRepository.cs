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

		private bool ProductsTableExists()
			{
			var command = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name=:name");
			command.Parameters.AddWithValue("name", "products");
			var dt = SqlLiteDatabaseHelpers.GetDataTable(command, ConnectionString);
			if (dt.Rows.Count > 0) return true; else return false;
			}

		public void Initialize()
			{
			if (!ProductsTableExists())
				{
				var command = new SQLiteCommand("create table products (id integer, name text, description text, price real)");
				SqlLiteDatabaseHelpers.ExecuteNonQuery(command, ConnectionString);
				}
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

		public List<Product> GetAllProducts()
			{
			var command = new SQLiteCommand("select id, name, description, price from products");
			var dt = SqlLiteDatabaseHelpers.GetDataTable(command, ConnectionString);
			return dt.AsEnumerable().Select(r => new Product()
			{
				Id = Convert.ToInt32(r.Field<long>("id")),
				Name = r.Field<string>("name"),
				Description = r.Field<string>("description"),
				Price = r.Field<double>("price")
			}).ToList();
			}

		public void AddNewProduct(Product product)
			{
			var command = new SQLiteCommand("insert into products (id, name, description, price) values (:id, :name, :description, :price)");
			command.Parameters.AddWithValue("id", product.Id);
			command.Parameters.AddWithValue("name", product.Name);
			command.Parameters.AddWithValue("description", product.Description);
			command.Parameters.AddWithValue("price", product.Price);
			SqlLiteDatabaseHelpers.ExecuteNonQuery(command, ConnectionString);
			}

		public void UpdateProduct(Product product)
			{
			var command = new SQLiteCommand("update products set name=:name, description=:description, price=:price where id=:id");
			command.Parameters.AddWithValue("id", product.Id);
			command.Parameters.AddWithValue("name", product.Name);
			command.Parameters.AddWithValue("description", product.Description);
			command.Parameters.AddWithValue("price", product.Price);
			SqlLiteDatabaseHelpers.ExecuteNonQuery(command, ConnectionString);
			}

		public void DeleteProduct(int id)
			{
			var command = new SQLiteCommand("delete from products where id=:id");
			command.Parameters.AddWithValue("id", id);
			SqlLiteDatabaseHelpers.ExecuteNonQuery(command, ConnectionString);
			}
		}
	}
