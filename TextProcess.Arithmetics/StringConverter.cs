using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TextProcess.Common;

namespace TextProcess.Arithmetics
{
	/// <summary>
	/// Class converter.
	/// </summary>
	public static class StringConverter
	{
		public static readonly int DecadeForMultiply = 10;
		public static readonly int ChunkSize = 15;

		/// <summary>
		/// Converts string to int.
		/// </summary>
		/// <param name="text">Specified string.</param>
		/// <returns>Converted integer.</returns>
		public static int ConvertToInt(this string text)
	    {
			Guard.ThrowIfConvertibleToNumber(text);
			Guard.ThrowIfInt(text);
		    return (int) PerformCalculations(text);
	    }

		/// <summary>
		/// Converts string to long.
		/// </summary>
		/// <param name="text">Specified string.</param>
		/// <returns>Converted long.</returns>
	    public static long ConvertToLong(this string text)
	    {
			Guard.ThrowIfConvertibleToNumber(text);
			Guard.ThrowIfLong(text);
		    return (long) PerformCalculations(text);
	    }

		/// <summary>
		/// Creates a number.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
	    private static BigInteger PerformCalculations(string text)
		{
			var charArr = text.ToCharArray();
			var editedArray = charArr.DeleteNegativeAndPositiveCharacters();

			var number = editedArray
				.Select((t, i) => (t - '0') * BigInteger.Pow(DecadeForMultiply, editedArray.Length - 1 - i))
				.Aggregate((currSum, item) => currSum + item);

			return text.Contains('-') ? number * -1 : number;
		}

		/// <summary>
		/// Deletes minus from an array.
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
	    private static char[] DeleteNegativeAndPositiveCharacters(this IEnumerable<char> array)
	    {
		    var list = array.ToList();
		    list.RemoveAll(x => x == '-' || x == '+');
		    return list.ToArray();
	    }
	}
}