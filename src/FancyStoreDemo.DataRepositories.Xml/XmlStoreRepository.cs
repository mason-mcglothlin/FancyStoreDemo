using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FancyStoreDemo.DataRepositories.Common;
using FancyStoreDemo.Models;

namespace FancyStoreDemo.DataRepositories.Xml
	{
	public class XmlStoreRepository : IStoreRepository
		{
		private string BaseAppDataPath { get; set; }
		private const string ProductsFolderName = "Products";
		public XmlStoreRepository(string baseAppDataPath)
			{
			BaseAppDataPath = baseAppDataPath;
			}
		public void Initialize()
			{
			var productsFolder = Path.Combine(BaseAppDataPath, ProductsFolderName);
			if (!Directory.Exists(productsFolder))
				{
				Directory.CreateDirectory(productsFolder);
				}
			}
		public Product GetProductById(int id)
			{
			var file = Path.Combine(BaseAppDataPath, ProductsFolderName, id.ToString(), ".xml");
			if (File.Exists(file))
				{
				var xml = File.ReadAllText(file);
				var product = XmlSerializationHelper.DeserializeObject<Product>(xml);
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
			foreach (var file in Directory.EnumerateFiles(folder, "*.xml"))
				{
				var xml = File.ReadAllText(file);
				var product = XmlSerializationHelper.DeserializeObject<Product>(xml);
				products.Add(product);
				}
			return products;
			}
		public void AddNewProduct(Product product)
			{
			var file = Path.Combine(BaseAppDataPath, ProductsFolderName, product.Id.ToString() + ".xml");
			var xml = XmlSerializationHelper.SerializeObject(product);
			File.WriteAllText(file, xml);
			}
		public void UpdateProduct(Product product)
			{
			var file = Path.Combine(BaseAppDataPath, ProductsFolderName, product.Id.ToString() + ".xml");
			var xml = XmlSerializationHelper.SerializeObject(product);
			File.WriteAllText(file, xml);
			}
		public void DeleteProduct(int id)
			{
			var file = Path.Combine(BaseAppDataPath, ProductsFolderName, id.ToString() + ".xml");
			File.Delete(file);
			}
		}
	}
