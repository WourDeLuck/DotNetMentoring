using System.Collections.Generic;
using System.IO;
using FileWatch.Interfaces;

namespace FileWatch.Services
{
	/// <summary>
	/// File system methods class wrapper for testing purposes.
	/// </summary>
	public class FileSystemWrapper : IFileSystemWrapper
	{
		public IEnumerable<string> GetFiles(string path)
		{
			return Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly);
		}

		public IEnumerable<string> GetDirectories(string path)
		{
			return Directory.GetDirectories(path);
		}
	}
}