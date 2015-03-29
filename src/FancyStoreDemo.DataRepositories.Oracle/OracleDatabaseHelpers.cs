using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace FancyStoreDemo.DataRepositories.Oracle
	{
	public static class OracleDatabaseHelpers
		{
		public static DataTable GetDataTable(OracleCommand command, string connectionString)
			{
			DataTable dt = new DataTable();
			using (var connection = new OracleConnection(connectionString))
				{
				command.Connection = connection;
				connection.Open();
				dt.Load(command.ExecuteReader());
				}
			return dt;
			}

		public static void ExecuteNonQuery(OracleCommand command, string connectionString)
			{
			using (var connection = new OracleConnection(connectionString))
				{
				command.Connection = connection;
				connection.Open();
				command.ExecuteNonQuery();
				}
			}
		}
	}
