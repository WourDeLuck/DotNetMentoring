using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TextProcess.Common
{
	/// <summary>
	/// Class validator.
	/// </summary>
	public static class Guard
	{
		/// <summary>
		/// Checks if specified path exists.
		/// </summary>
		/// <param name="path">Path to the file.</param>
		public static void ThrowPathExistence(string path)
		{
			if (!File.Exists(path))
			{
				throw new FileNotFoundException("Specified file was not found.", path);
			}
		}

		/// <summary>
		/// Checks if string can be converted to a number.
		/// </summary>
		/// <param name="text">Specified text.</param>
		public static void ThrowIfConvertibleToNumber(string text)
		{
			if (!Regex.IsMatch(text, "^[+-]?\\d*$"))
			{
				throw new ArgumentException("The string does not represent a correct number.", text);
			}
		}

		/// <summary>
		/// Checks if string can be converted to int.
		/// </summary>
		/// <param name="text">Specified text.</param>
		public static void ThrowIfInt(string text)
		{
			var rangeValueArr = text.Contains('-') ? int.MinValue.ToString().ToCharArray() : int.MaxValue.ToString().ToCharArray();
			var charArr = text.ToCharArray();

			RangeCheck(charArr, rangeValueArr.DeleteNegativeAndPositiveCharacters().Length);
			ValueCheck(charArr, rangeValueArr);
		}

		/// <summary>
		/// Checks if string can be converted to long.
		/// </summary>
		/// <param name="text">Specified text.</param>
		public static void ThrowIfLong(string text)
		{
			var rangeValueArr = text.Contains('-') ? long.MinValue.ToString().ToCharArray() : long.MaxValue.ToString().ToCharArray();
			var charArr = text.ToCharArray();

			RangeCheck(charArr, rangeValueArr.DeleteNegativeAndPositiveCharacters().Length);
			ValueCheck(charArr, rangeValueArr);
		}

		/// <summary>
		/// Compares array length to maximum possible length.
		/// </summary>
		/// <param name="arr"></param>
		/// <param name="maxLength"></param>
		private static void RangeCheck(char[] arr, int maxLength)
		{
			var listOfChars = arr.DeleteNegativeAndPositiveCharacters().ToList();

			if (listOfChars.Count > maxLength)
			{
				throw new IndexOutOfRangeException("Unable to convert string to number: Amount of numbers is bigger than maximum possible.");
			}
		}

		/// <summary>
		/// Compares two arrays by their values.
		/// </summary>
		/// <param name="originalArr"></param>
		/// <param name="arrForCompare"></param>
		private static void ValueCheck(char[] originalArr, char[] arrForCompare)
		{
			var numericOriginalArr = originalArr.DeleteNegativeAndPositiveCharacters().ToList();
			var numericArrForCompare = arrForCompare.DeleteNegativeAndPositiveCharacters().ToList();

			if (numericOriginalArr.Count != numericArrForCompare.Count) return;
			for (var i = 0; i < numericOriginalArr.Count; i++)
			{
				if (numericOriginalArr[i] - '0' == numericArrForCompare[i] - '0') continue;
				if (numericOriginalArr[i] - '0' < numericArrForCompare[i] - '0') break;
				if (numericOriginalArr[i] - '0' > numericArrForCompare[i] - '0')
				{
					throw new IndexOutOfRangeException("Unable to convert string to number: Value is out of range.");
				}
			}
		}
	}
}