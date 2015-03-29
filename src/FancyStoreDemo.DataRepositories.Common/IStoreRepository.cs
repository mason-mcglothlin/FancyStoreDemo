using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FancyStoreDemo.Models;

namespace FancyStoreDemo.DataRepositories.Common
{
	public interface IStoreRepository
		{
		Product GetProductById(int id);

		void Initialize();
		}
}
