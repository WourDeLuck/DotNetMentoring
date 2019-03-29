using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Extensions
{
	public static class CollectionExtensions
	{
		public static void WriteToApp<T>(this IEnumerable<T> collection)
		{
			foreach (var element in collection)
			{
				ObjectDumper.Write(element);
			}
		}
	}
}
