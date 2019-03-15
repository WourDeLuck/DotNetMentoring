using System;
using System.IO;
using System.Linq;

namespace TextProcess.Common
{
	public static class Guard
	{
		public static void CheckPathExistence(string path)
		{
			if (!File.Exists(path))
			{
				throw new FileNotFoundException("Specified file was not found.", path);
			}
		}

		public static void CheckIfStringIsNumeric(string text)
		{
			if (!text.All(char.IsNumber))
			{
				throw new ArgumentException("Specified string is not a number", text);
			}
		}
	}
}
