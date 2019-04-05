using System.Collections.Generic;
using System.Configuration;
using System.Globalization;

namespace FileSystemService.Common.Models
{
	public class StartSettings : ConfigurationElement
	{
		[ConfigurationProperty("uiLanguage")]
		public CultureInfo UiCulture => (CultureInfo) this["uiLanguage"];

		[ConfigurationProperty("folders")]
		public FolderCollection FoldersToListen { get; set; }

		[ConfigurationProperty("rules")]
		public AcceptanceRuleCollection AcceptanceRules => (AcceptanceRuleCollection) this["rules"];
	}
}
