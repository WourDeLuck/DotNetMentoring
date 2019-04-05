using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
