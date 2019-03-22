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
		public static void ThrowIfLong(string text)
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
			var listComp = originalArr.ToList();
			listComp.Remove('-');

			if (listComp.Count != arrForCompare.Length) return;
			for (var i = 0; i < listComp.Count; i++)
			{
				if (listComp[i] - '0' == arrForCompare[i] - '0') continue;
				if (listComp[i] - '0' < arrForCompare[i] - '0') break;
				if (listComp[i] - '0' > arrForCompare[i] - '0')
				{
					throw new IndexOutOfRangeException("Unable to convert string to number: Value is out of range.");
				}
			}
		}
	}
}