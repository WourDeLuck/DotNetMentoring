using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemService.Common.Models
{
	public class Folder : ConfigurationElement
	{
		[ConfigurationProperty("folderPath", IsKey = true)]
		public string Path { get; set; }
	}
}
