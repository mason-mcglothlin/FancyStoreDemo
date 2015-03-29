using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FancyStoreDemo.DataRepositories.Common;
using FancyStoreDemo.Models;
using Ninject;

namespace FancyStoreDemo.PublicWebSite
	{
	public partial class AddProduct : System.Web.UI.Page
		{
		[Inject]
		public IStoreRepository Repo { get; set; }

		protected void Page_Load(object sender, EventArgs e)
			{

			}

		protected void AddProductBtn_Click(object sender, EventArgs e)
			{
			Product product = new Product()
			{
				Id = Int32.Parse(IdTB.Text),
				Name = NameTB.Text,
				Description = DescriptionTB.Text,
				Price = Double.Parse(PriceTB.Text)
			};
			Repo.AddNewProduct(product);
			IdTB.Text = "";
			NameTB.Text = "";
			DescriptionTB.Text = "";
			PriceTB.Text = "";
			}
		}
	}