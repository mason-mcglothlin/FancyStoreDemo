using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancyStoreDemo.DataRepositories.SqlLite
	{
	public static class SqlLiteDatabaseHelpers
		{
		public static DataTable GetDataTable(SQLiteCommand command, string connectionString)
			{
			DataTable dt = new DataTable();
			using (var connection = new SQLiteConnection(connectionString))
				{
				command.Connection = connection;
				connection.Open();
				dt.Load(command.ExecuteReader());
				}
			return dt;
			}

		public static void ExecuteNonQuery(SQLiteCommand command, string connectionString)
			{
			using (var connection = new SQLiteConnection(connectionString))
				{
				command.Connection = connection;
				connection.Open();
				command.ExecuteNonQuery();
				}
			}
		}
	}
