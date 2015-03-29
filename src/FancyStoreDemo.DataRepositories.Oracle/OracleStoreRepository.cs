using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FancyStoreDemo.DataRepositories.Common;
using FancyStoreDemo.Models;
using Oracle.ManagedDataAccess.Client;

namespace FancyStoreDemo.DataRepositories.Oracle
	{
	public class OracleStoreRepository : IStoreRepository
		{
		private string ConnectionString { get; set; }

		public OracleStoreRepository(string connectionString)
			{
			ConnectionString = connectionString;
			}

		public void Initialize()
			{
			throw new NotImplementedException();
			OracleCommand command = new OracleCommand("create table products ()");
			OracleDatabaseHelpers.ExecuteNonQuery(command, ConnectionString);
			}

		public Product GetProductById(int id)
			{
			var command = new OracleCommand("select id, name, description, price from products where id=:id");
			command.Parameters.Add("id", id);
			command.BindByName = true; //not stricly necessary but if had more than one parameter this helps avoid confusion. True is also the default for MS System.Data.Sql
			var dt = OracleDatabaseHelpers.GetDataTable(command, ConnectionString);
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
			var command = new OracleCommand("select id, name, description, price from products");
			var dt = OracleDatabaseHelpers.GetDataTable(command, ConnectionString);
			return dt.AsEnumerable().Select(r => new Product()
			{
				Id = r.Field<int>("id"),
				Name = r.Field<string>("name"),
				Description = r.Field<string>("description"),
				Price = r.Field<double>("price")
			}).ToList();
			}

		public void AddNewProduct(Product product)
			{
			var command = new OracleCommand("insert into products (id, name, description, price) values (:id, :name, :description, :price)");
			command.BindByName = true;
			command.Parameters.Add("id", product.Id);
			command.Parameters.Add("name", product.Name);
			command.Parameters.Add("description", product.Description);
			command.Parameters.Add("price", product.Price);
			OracleDatabaseHelpers.ExecuteNonQuery(command, ConnectionString);
			}

		public void UpdateProduct(Product product)
			{
			var command = new OracleCommand("update products set name=:name, description=:description, price=:price where id=:id");
			command.BindByName = true;
			command.Parameters.Add("id", product.Id);
			command.Parameters.Add("name", product.Name);
			command.Parameters.Add("description", product.Description);
			command.Parameters.Add("price", product.Price);
			OracleDatabaseHelpers.ExecuteNonQuery(command, ConnectionString);
			}

		public void DeleteProduct(int id)
			{
			var command = new OracleCommand("delete from products where id=:id");
			command.BindByName = true;
			command.Parameters.Add("id", id);
			OracleDatabaseHelpers.ExecuteNonQuery(command, ConnectionString);
			}
		}
	}