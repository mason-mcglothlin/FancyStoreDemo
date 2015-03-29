using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FancyStoreDemo.DataRepositories.Common;
using FancyStoreDemo.Models;
using Newtonsoft.Json;

namespace FancyStoreDemo.DataRepositories.AppDataFiles
	{
	public class AppDataFilesStoreRepository : IStoreRepository
		{
		private string BaseAppDataPath { get; set; }
		private const string ProductsFolderName = "Products";
		public AppDataFilesStoreRepository()
			{
			BaseAppDataPath = HttpContext.Current.Server.MapPath("~/App_Data");
			var productsFolder = Path.Combine(BaseAppDataPath, ProductsFolderName);
			if (!Directory.Exists(productsFolder))
				{
				Directory.CreateDirectory(productsFolder);
				}
			}
		public void Initialize()
			{
			}
		public Product GetProductById(int id)
			{
			var file = Path.Combine(BaseAppDataPath, ProductsFolderName, id.ToString(), ".json");
			if (File.Exists(file))
				{
				var json = File.ReadAllText(file);
				var product = JsonConvert.DeserializeObject<Product>(json);
				return product;
				}
			else
				{
				return null;
				}
			}
		public List<Product> GetAllProducts()
			{
			var folder = Path.Combine(BaseAppDataPath, ProductsFolderName);
			var products = new List<Product>();
			foreach (var file in Directory.EnumerateFiles(folder))
				{
				var json = File.ReadAllText(file);
				var product = JsonConvert.DeserializeObject<Product>(json);
				products.Add(product);
				}
			return products;
			}
		public void AddNewProduct(Product product)
			{
			var file = Path.Combine(BaseAppDataPath, ProductsFolderName, product.Id.ToString() + ".json");
			var json = JsonConvert.SerializeObject(product);
			File.WriteAllText(file, json);
			}
		public void UpdateProduct(Product product)
			{
			var file = Path.Combine(BaseAppDataPath, ProductsFolderName, product.Id.ToString() + ".json");
			var json = JsonConvert.SerializeObject(product);
			File.WriteAllText(file, json);
			}
		public void DeleteProduct(int id)
			{
			var file = Path.Combine(BaseAppDataPath, ProductsFolderName, id.ToString() + ".json");
			File.Delete(file);
			}
		}
	}
