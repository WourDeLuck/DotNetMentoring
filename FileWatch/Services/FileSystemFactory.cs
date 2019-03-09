using System.Collections.Generic;
using System.IO;
using FileWatch.Interfaces;

namespace FileWatch.Services
{
	/// <summary>
	/// File system methods class wrapper for testing purposes.
	/// </summary>
	public class FileSystemFactory : ISystemFactory
	{
		public IEnumerable<string> GetFileSystemContent(string path)
		{
			return Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
		}
	}
}