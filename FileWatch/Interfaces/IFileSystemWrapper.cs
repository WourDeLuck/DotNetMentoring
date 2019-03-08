using System.Collections.Generic;

namespace FileWatch.Interfaces
{
	public interface IFileSystemWrapper
	{
		IEnumerable<string> GetFiles(string path);

		IEnumerable<string> GetDirectories(string path);
	}
}