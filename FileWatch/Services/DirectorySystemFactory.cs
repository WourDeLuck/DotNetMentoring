using System.Collections.Generic;
using System.IO;
using FileWatch.Interfaces;

namespace FileWatch.Services
{
	/// <inheritdoc />
	public class DirectorySystemFactory : ISystemFactory
	{
		/// <inheritdoc />
		public IEnumerable<string> GetFileSystemContent(string path) => Directory.GetDirectories(path, "*", SearchOption.AllDirectories);
	}
}
