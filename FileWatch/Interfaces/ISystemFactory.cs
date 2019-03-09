using System.Collections.Generic;

namespace FileWatch.Interfaces
{
	/// <summary>
	/// Factory fore getting files and folders.
	/// </summary>
	public interface ISystemFactory
	{
		/// <summary>
		/// Gets file system content
		/// </summary>
		/// <param name="path">Start point.</param>
		/// <returns>Collection of file system content.</returns>
		IEnumerable<string> GetFileSystemContent(string path);
	}
}