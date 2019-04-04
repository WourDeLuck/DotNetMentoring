using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemService.Common.Models
{
	public class StartSettings
	{
		public string UiCulture { get; set; }

		public IList<string> FoldersToListen { get; set; }

		public IList<AcceptanceRule> AcceptanceRules { get; set; }
	}
}
