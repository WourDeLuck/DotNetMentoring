using System;
using System.Collections.Generic;
using System.Linq;
using TextProcess.Common;

namespace TextProcess.Arithmetics
{
	/// <summary>
	/// Class converter.
	/// </summary>
	public class StringConverter
    {
		/// <summary>
		/// Converts string to int.
		/// </summary>
		/// <param name="text">Specified string.</param>
		/// <returns>Converted integer.</returns>
		public int ConvertToInt(string text)
	    {
			Guard.CheckIfConvertableToNumber(text);
			Guard.CheckIfInt(text);
		    return (int) PerformCalculations(text);
	    }

		/// <summary>
		/// Converts string to long.
		/// </summary>
		/// <param name="text">Specified string.</param>
		/// <returns>Converted long.</returns>
	    public long ConvertToLong(string text)
	    {
			Guard.CheckIfConvertableToNumber(text);
			Guard.CheckIfLong(text);
		    return (long) PerformCalculations(text);
	    }

		/// <summary>
		/// Creates a number.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
	    private double PerformCalculations(string text)
	    {
		    var decadeForMultiply = 10;
			var charArr = DeleteMinus(text.ToCharArray());

			var finalNumber = charArr
				.Select((t, i) => (t - '0') * Math.Pow(decadeForMultiply, charArr.Length - 1 - i))
				.Sum();

		    return text.Contains('-') ? finalNumber * -1 : finalNumber;
	    }

		/// <summary>
		/// Deletes minus from an array.
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
	    private char[] DeleteMinus(char[] array)
	    {
		    var list = new List<char>(array);
		    list.Remove('-');
		    list.Remove('+');
		    return list.ToArray();
	    }
	}
}