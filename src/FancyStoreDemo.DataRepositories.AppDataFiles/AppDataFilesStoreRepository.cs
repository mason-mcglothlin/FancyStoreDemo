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
			var productsFolder = Path.Combine(BaseAppDataPath, ProductsFolderName);
			var filename = Path.Combine(productsFolder, id.ToString(), ".json");
			if (File.Exists(filename))
				{
				var json = File.ReadAllText(filename);
				var product = JsonConvert.DeserializeObject<Product>(json);
				return product;
				}
			else
				{
				return null;
				}
			}
		}
	}
