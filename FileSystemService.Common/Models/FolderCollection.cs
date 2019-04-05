using System.Configuration;

namespace FileSystemService.Common.Models
{
	public class FolderCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new Folder();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((Folder) element).Path;
		}
	}
}
