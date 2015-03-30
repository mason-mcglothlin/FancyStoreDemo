using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FancyStoreDemo.DataRepositories.Xml
	{
	public class XmlSerializationHelper
		{
		public static string SerializeObject<T>(T dataToSerialize)
			{
			var stringwriter = new System.IO.StringWriter();
			var serializer = new XmlSerializer(typeof(T));
			serializer.Serialize(stringwriter, dataToSerialize);
			return stringwriter.ToString();
			}

		public static T DeserializeObject<T>(string xmlText)
			{
			var stringReader = new System.IO.StringReader(xmlText);
			var serializer = new XmlSerializer(typeof(T));
			return (T)serializer.Deserialize(stringReader);
			}
		}
	}
