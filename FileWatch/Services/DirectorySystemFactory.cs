using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileWatch.Interfaces;

namespace FileWatch.Services
{
	public class DirectorySystemFactory : ISystemFactory
	{
		public IEnumerable<string> GetFileSystemContent(string path)
		{
			return Directory.GetDirectories(path, "*", SearchOption.AllDirectories);
		}
	}
}
