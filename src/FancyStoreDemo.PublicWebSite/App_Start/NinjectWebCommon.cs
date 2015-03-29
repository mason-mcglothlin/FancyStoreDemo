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

	public static class NinjectWebCommon
		{
		/// <summary>
		/// This is where we tell our application what class to use for each dependency.
		/// </summary>
		/// <param name="kernel">The kernel.</param>
		private static void RegisterServices(IKernel kernel)
			{
			//kernel.Bind<IStoreRepository>().To<AppDataFilesStoreRepository>();
			//kernel.Bind<IStoreRepository>().To<InMemoryStoreRepository>().InSingletonScope();
			kernel.Bind<IStoreRepository>().To<MongoStoreRepository>().WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString);
			//kernel.Bind<IStoreRepository>().To<OracleStoreRepository>();
			//kernel.Bind<IStoreRepository>().To<SqlLiteStoreRepository>();
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
