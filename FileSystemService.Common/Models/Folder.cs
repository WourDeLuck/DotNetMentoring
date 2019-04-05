using System.Configuration;

namespace FileSystemService.Common.Models
{
	public class Folder : ConfigurationElement
	{
		[ConfigurationProperty("path", IsKey = true)]
		public string Path => (string)base["path"];
	}
}
