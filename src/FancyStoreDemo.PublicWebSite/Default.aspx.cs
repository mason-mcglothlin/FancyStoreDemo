using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FancyStoreDemo.DataRepositories.Common;
using FancyStoreDemo.Models;
using Ninject;
using SharpNoty;

namespace FancyStoreDemo.PublicWebSite
	{
	public partial class Default : System.Web.UI.Page
		{
		[Inject]
		public IStoreRepository Repo {get; set;}

		protected void Page_Load(object sender, EventArgs e)
			{

			}

		protected void SeedDatabaseBtn_Click(object sender, EventArgs e)
			{
			Repo.AddNewProduct(new Product() { Id = 1, Name = "Toy", Description = "A fun toy.", Price = 5 });
			Repo.AddNewProduct(new Product() { Id = 2, Name = "Ball", Description = "A bouny ball.", Price = 2 });
			Repo.AddNewProduct(new Product() { Id = 3, Name = "Shoes", Description = "Spiffy shoes.", Price = 40 });
			Repo.AddNewProduct(new Product() { Id = 4, Name = "Hat", Description = "Sophisticated headwear.", Price = 10 });
			Repo.AddNewProduct(new Product() { Id = 5, Name = "Table", Description = "Put things on it.", Price = 35 });
			Page.AddNoty(new Noty("Database seeded.", MessageType.success));
			}
		}
	}