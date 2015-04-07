using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FancyStoreDemo.Models;
using FluentNHibernate.Mapping;

namespace FancyStoreDemo.DataRepositories.NHibernate
	{
	public class ProductMap : ClassMap<Product>
		{
		public ProductMap()
			{
			Id(p => p.Id);
			Map(p => p.Name);
			Map(p => p.Description);
			Map(p => p.Price);
			}
		}
	}
