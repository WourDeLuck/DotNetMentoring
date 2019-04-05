using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemService.Common
{
	public static class Guard
	{
		public static void ThrowFileExistence(string filePath)
		{
			if (!File.Exists(filePath))
			{
				throw new FileNotFoundException($"Specified file not found: {filePath}");
			}
		}

		public static void ThrowDirectoryExistence(string folderLink)
		{
			if (!Directory.Exists(folderLink))
			{
				throw new DirectoryNotFoundException($"Specified directory wasn't found: {folderLink}");
			}
		}
	}
}
