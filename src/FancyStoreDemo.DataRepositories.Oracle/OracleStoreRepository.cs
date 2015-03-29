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
		}
	}