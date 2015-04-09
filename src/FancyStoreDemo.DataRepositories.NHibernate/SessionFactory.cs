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
		public static ISessionFactory CreateSessionFactory(string sqliteFileName)
			{
			return Fluently.Configure()
			  .Database(
				SQLiteConfiguration.Standard
				  .UsingFile(sqliteFileName)
			  )
			  .Mappings(m =>
				m.FluentMappings.AddFromAssemblyOf<ProductMap>())
			  .BuildSessionFactory();
			}
		}
	}
