using System;
using System.IO;
using System.Net;

namespace WebsiteCrawler.Helpers
{
	public static class Guard
	{
		public static void ThrowIfDirectoryNotExist(string folderPath)
		{
			if (!Directory.Exists(folderPath))
			{
				throw new ArgumentException("Specified folder wasn't found.");
			}
		}

		public static void ThrowIfStringIsNullOrEmpty(string originalString)
		{
			if (string.IsNullOrEmpty(originalString))
			{
				throw new ArgumentNullException(nameof(originalString));
			}
		}

		public static void ThrowIfUriIsInvalid(string uri)
		{
			if (!Uri.IsWellFormedUriString(uri, UriKind.RelativeOrAbsolute))
			{
				throw new ArgumentException("Uri is in incorrect format.");
			}
		}

		public static void ThrowIfFileNotExist(string filePath)
		{
			if (!File.Exists(filePath))
			{
				throw new ArgumentException("Specified file wasn't found");
			}
		}
	}
}
