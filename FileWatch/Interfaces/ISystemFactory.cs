using System.Collections.Generic;

namespace FileWatch.Interfaces
{
	public interface ISystemFactory
	{
		IEnumerable<string> GetFileSystemContent(string path);
	}
}