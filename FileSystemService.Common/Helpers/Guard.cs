using System.IO;
using exceptions = FileSystemService.Common.Resources.Exceptions;

namespace FileSystemService.Common.Helpers
{
	public static class Guard
	{
		public static void ThrowFileExistence(string filePath)
		{
			if (!File.Exists(filePath))
			{
				throw new FileNotFoundException($"{exceptions.FileNotFoundExc}: {filePath}");
			}
		}

		public static void ThrowDirectoryExistence(string folderLink)
		{
			if (!Directory.Exists(folderLink))
			{
				throw new DirectoryNotFoundException($"{exceptions.DirectoryNotFoundExc}: {folderLink}");
			}
		}
	}
}
