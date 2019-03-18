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
		public static void CheckPathExistence(string path)
		{
			if (!File.Exists(path))
			{
				throw new FileNotFoundException("Specified file was not found.", path);
			}
		}

		/// <summary>
		/// Check if string is numeric.
		/// </summary>
		/// <param name="text">Specified text.</param>
		public static void CheckIfStringIsNumeric(string text)
		{
			if (!text.All(char.IsNumber))
			{
				throw new ArgumentException("Specified string is not a number", text);
			}
		}

		/// <summary>
		/// Checks if string can be converted to a number.
		/// </summary>
		/// <param name="text">Specified text.</param>
		public static void CheckIfConvertableToNumber(string text)
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
		public static void CheckIfInt(string text)
		{
			var maxValueLength = int.MaxValue.ToString().Length;
			var charArr = text.ToCharArray();

			RangeCheck(charArr, maxValueLength);

			var maxValArr = int.MaxValue.ToString().ToCharArray();
			ValueCheck(charArr, maxValArr);
		}

		/// <summary>
		/// Checks if string can be converted to long.
		/// </summary>
		/// <param name="text">Specified text.</param>
		public static void CheckIfLong(string text)
		{
			var maxValueLength = long.MaxValue.ToString().Length;
			var charArr = text.ToCharArray();

			RangeCheck(charArr, maxValueLength);

			var maxValArr = long.MaxValue.ToString().ToCharArray();
			ValueCheck(charArr, maxValArr);
		}

		/// <summary>
		/// Compares array length to maximum possible length.
		/// </summary>
		/// <param name="arr"></param>
		/// <param name="maxLength"></param>
		private static void RangeCheck(char[] arr, int maxLength)
		{
			var listOfChars = arr.ToList();
			listOfChars.Remove('-');

			if (arr.Length > maxLength)
			{
				throw new IndexOutOfRangeException("Unable to convert string to integer: Value is too big.");
			}
		}

		/// <summary>
		/// Compares two arrays by their values.
		/// </summary>
		/// <param name="originalArr"></param>
		/// <param name="arrForCompar"></param>
		private static void ValueCheck(char[] originalArr, char[] arrForCompar)
		{
			var listComp = originalArr.ToList();
			listComp.Remove('-');

			if (listComp.Count == arrForCompar.Length)
			{
				if (listComp.Where((t, i) => t - '0' > arrForCompar[i] - '0').Any())
				{
					throw new IndexOutOfRangeException("Unable to convert string to number: Value is too big.");
				}
			}
		}
	}
}