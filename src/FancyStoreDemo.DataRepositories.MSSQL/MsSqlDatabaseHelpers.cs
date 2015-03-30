using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancyStoreDemo.DataRepositories.MSSQL
	{
	public class MsSqlDatabaseHelpers
		{
		public static DataTable GetDataTable(SqlCommand command, string connectionString)
			{
			DataTable dt = new DataTable();
			using (var connection = new SqlConnection(connectionString))
				{
				command.Connection = connection;
				connection.Open();
				dt.Load(command.ExecuteReader());
				}
			return dt;
			}

		public static void ExecuteNonQuery(SqlCommand command, string connectionString)
			{
			using (var connection = new SqlConnection(connectionString))
				{
				command.Connection = connection;
				connection.Open();
				command.ExecuteNonQuery();
				}
			}
		}
	}
