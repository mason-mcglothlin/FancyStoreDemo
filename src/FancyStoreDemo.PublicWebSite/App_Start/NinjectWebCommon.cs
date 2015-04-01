[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(FancyStoreDemo.PublicWebSite.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(FancyStoreDemo.PublicWebSite.App_Start.NinjectWebCommon), "Stop")]

namespace FancyStoreDemo.PublicWebSite.App_Start
	{
	using System;
	using System.Web;
	using FancyStoreDemo.DataRepositories.Common;
	using FancyStoreDemo.DataRepositories.AppDataFiles;
	using FancyStoreDemo.DataRepositories.InMemory;
	using FancyStoreDemo.DataRepositories.Mongo;
	using FancyStoreDemo.DataRepositories.Oracle;
	using FancyStoreDemo.DataRepositories.SqlLite;
	using Microsoft.Web.Infrastructure.DynamicModuleHelper;
	using Ninject;
	using Ninject.Web.Common;
	using System.Configuration;
	using System.Web.Hosting;
	using FancyStoreDemo.DataRepositories.Xml;
	using FancyStoreDemo.DataRepositories.RavenDB;
	using FancyStoreDemo.DataRepositories.MSSQL;
	using FancyStoreDemo.DataRepositories.Redis;

	public static class NinjectWebCommon
		{
		/// <summary>
		/// This is where we tell our application what class to use for each dependency.
		/// </summary>
		/// <param name="kernel">The kernel.</param>
		private static void RegisterServices(IKernel kernel)
			{
			var storageType = ConfigurationManager.AppSettings["DataStorageType"];
			switch (storageType)
				{
				case "JSON":
				new JsonStoreRepository(HostingEnvironment.MapPath("~/App_Data")).Initialize();
				kernel.Bind<IStoreRepository>().ToMethod(c => new JsonStoreRepository(HostingEnvironment.MapPath("~/App_Data")));
				break;

				case "XML":
				new XmlStoreRepository(HostingEnvironment.MapPath("~/App_Data")).Initialize();
				kernel.Bind<IStoreRepository>().ToMethod(c => new XmlStoreRepository(HostingEnvironment.MapPath("~/App_Data")));
				break;

				case "RavenDB":
				kernel.Bind<IStoreRepository>().ToMethod(c => new RavenStoreRepository(HostingEnvironment.MapPath("~/App_Data/FancyStoreRaven"))).InSingletonScope();
				break;

				case "InMemory":
				kernel.Bind<IStoreRepository>().To<InMemoryStoreRepository>().InSingletonScope();
				break;

				case "Redis":
				kernel.Bind<IStoreRepository>().ToMethod(c => new RedisStoreRepository(ConfigurationManager.ConnectionStrings["Redis"].ConnectionString)).InSingletonScope();
				break;

				case "MongoDB":
				kernel.Bind<IStoreRepository>().ToMethod(c => new MongoStoreRepository(ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString));
				break;

				case "MSSQL":
				new MsSqlStoreRepository(ConfigurationManager.ConnectionStrings["MsSql"].ConnectionString).Initialize();
				kernel.Bind<IStoreRepository>().ToMethod(c => new MsSqlStoreRepository(ConfigurationManager.ConnectionStrings["MsSql"].ConnectionString));
				break;

				case "Oracle":
				new OracleStoreRepository(ConfigurationManager.ConnectionStrings["Oracle"].ConnectionString).Initialize();
				kernel.Bind<IStoreRepository>().ToMethod(c => new OracleStoreRepository(ConfigurationManager.ConnectionStrings["Oracle"].ConnectionString));
				break;

				case "SQLite":
				new SqlLiteStoreRepository(ConfigurationManager.ConnectionStrings["SQLite"].ConnectionString).Initialize();
				kernel.Bind<IStoreRepository>().ToMethod(c => new SqlLiteStoreRepository(ConfigurationManager.ConnectionStrings["SQLite"].ConnectionString));
				break;

				case "":
				throw new ApplicationException("Missing DataStorageType app setting.");

				default:
				throw new ApplicationException(String.Format("Unknown data storage type {0}. Verify the DataStorageType app setting is a valid value.", storageType));
				}
			}

		#region Boilerplate Ninject
		private static readonly Bootstrapper bootstrapper = new Bootstrapper();

		/// <summary>
		/// Starts the application
		/// </summary>
		public static void Start()
			{
			DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
			DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
			bootstrapper.Initialize(CreateKernel);
			}

		/// <summary>
		/// Stops the application.
		/// </summary>
		public static void Stop()
			{
			bootstrapper.ShutDown();
			}

		/// <summary>
		/// Creates the kernel that will manage your application.
		/// </summary>
		/// <returns>The created kernel.</returns>
		private static IKernel CreateKernel()
			{
			var kernel = new StandardKernel();
			try
				{
				kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
				kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

				RegisterServices(kernel);
				return kernel;
				}
			catch
				{
				kernel.Dispose();
				throw;
				}
			}
		#endregion
		}
	}
