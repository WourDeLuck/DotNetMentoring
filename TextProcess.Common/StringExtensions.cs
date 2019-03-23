using System.Collections.Generic;
using System.Linq;

namespace TextProcess.Common
{
	public static class StringExtensions
	{
		/// <summary>
		/// Deletes minus from an array.
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		public static char[] DeleteNegativeAndPositiveCharacters(this IEnumerable<char> array)
		{
			var list = array.ToList();
			list.RemoveAll(x => x == '-' || x == '+');
			return list.ToArray();
		}
	}
}