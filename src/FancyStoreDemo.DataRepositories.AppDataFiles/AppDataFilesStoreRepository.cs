using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FancyStoreDemo.DataRepositories.Common;
using FancyStoreDemo.Models;
using Newtonsoft.Json;

namespace FancyStoreDemo.DataRepositories.AppDataFiles
	{
	public class AppDataFilesStoreRepository : IStoreRepository
		{
		private string BaseAppDataPath { get; set; }
		private const string ProductsFolderName = "Products";
		public AppDataFilesStoreRepository(string AppDataPath)
			{
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
		public void AddNewProduct(Product product)
			{
			var file = Path.Combine(BaseAppDataPath, ProductsFolderName, product.Id.ToString(), ".json");
			var json = JsonConvert.SerializeObject(product);
			File.WriteAllText(file, json);
			}
		public void UpdateProduct(Product product)
			{
			var file = Path.Combine(BaseAppDataPath, ProductsFolderName, product.Id.ToString(), ".json");
			var json = JsonConvert.SerializeObject(product);
			File.WriteAllText(file, json);
			}
		public void DeleteProduct(int id)
			{
			var file = Path.Combine(BaseAppDataPath, ProductsFolderName, id.ToString(), ".json");
			File.Delete(file);
			}
		}
	}
