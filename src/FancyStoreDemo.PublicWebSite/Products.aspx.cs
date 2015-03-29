using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FancyStoreDemo.DataRepositories.Common;
using Ninject;

namespace FancyStoreDemo.PublicWebSite
	{
	public partial class _Default : Page
		{
		[Inject]
		public IStoreRepository Repo { get; set; }

		protected void Page_Load(object sender, EventArgs e)
			{
			if (!IsPostBack)
				{
				var products = Repo.GetAllProducts();
				if (products.Count == 0)
					{
					ProductsRepeater.Visible = false; EmptyProductsLabel.Visible = true;
					}
				else
					{
					ProductsRepeater.Visible = true; EmptyProductsLabel.Visible = false;
					ProductsRepeater.DataSource = products;
					ProductsRepeater.DataBind();
					}			
				}
			}
		}
	}