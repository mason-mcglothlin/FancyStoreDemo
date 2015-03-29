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

		private bool ProductsTableExists()
			{
			var command = new OracleCommand("SELECT table_name FROM user_tables where lower(table_name)=:name");
			command.BindByName = true;
			command.Parameters.Add("name", "products");
			var dt = OracleDatabaseHelpers.GetDataTable(command, ConnectionString);
			if (dt.Rows.Count > 0) return true; else return false;
			}

		public void Initialize()
			{
			if (!ProductsTableExists())
				{
				var command = new OracleCommand("create table products (id integer, name varchar2(60), description varchar2(500), price number)");
				OracleDatabaseHelpers.ExecuteNonQuery(command, ConnectionString);
				}
			}

		private Product GenerateProductFromDataRow(DataRow row)
			{
			return new Product()
			{
				Id = Convert.ToInt32(row.Field<decimal>("id")),
				Name = row.Field<string>("name"),
				Description = row.Field<string>("description"),
				Price = Convert.ToDouble(row.Field<decimal>("price"))
			};
			}

		public Product GetProductById(int id)
			{
			var command = new OracleCommand("select id, name, description, price from products where id=:id");
			command.Parameters.Add("id", id);
			command.BindByName = true; //not stricly necessary but if had more than one parameter this helps avoid confusion. True is also the default for MS System.Data.Sql
			var dt = OracleDatabaseHelpers.GetDataTable(command, ConnectionString);
			return dt.AsEnumerable().Select(r => GenerateProductFromDataRow(r)).Single();
			}

		public List<Product> GetAllProducts()
			{
			var command = new OracleCommand("select id, name, description, price from products");
			var dt = OracleDatabaseHelpers.GetDataTable(command, ConnectionString);
			return dt.AsEnumerable().Select(r => GenerateProductFromDataRow(r)).ToList();
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