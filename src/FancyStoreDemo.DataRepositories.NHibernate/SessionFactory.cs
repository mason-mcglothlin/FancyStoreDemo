using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace FancyStoreDemo.DataRepositories.NHibernate
	{
	public class SessionFactory
		{
		private static string DbFile = "nhibernate.db";

		public static ISessionFactory CreateSessionFactory()
			{
			return Fluently.Configure()
			  .Database(
				SQLiteConfiguration.Standard
				  .UsingFile(DbFile)
			  )
			  .Mappings(m =>
				m.FluentMappings.AddFromAssemblyOf<ProductMap>())
			  .ExposeConfiguration(BuildSchema)
			  .BuildSessionFactory();
			}
		private static void BuildSchema(Configuration config)
			{
			// delete the existing db on each run
			if (File.Exists(DbFile))
				File.Delete(DbFile);

			// this NHibernate tool takes a configuration (with mapping info in)
			// and exports a database schema from it
			new SchemaExport(config)
			  .Create(false, true);
			}
		}
	}
