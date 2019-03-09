using System.Collections.Generic;
using System.IO;
using FileWatch.Interfaces;

namespace FileWatch.Services
{
	/// <inheritdoc />
	public class FileSystemFactory : ISystemFactory
	{
		/// <inheritdoc />
		public IEnumerable<string> GetFileSystemContent(string path) => Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
	}
}