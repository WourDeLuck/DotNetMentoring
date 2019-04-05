using System.Configuration;
using System.Globalization;

namespace FileSystemService.Common.Models
{
	public class StartSettings : ConfigurationSection
	{
		[ConfigurationProperty("uiLanguage")]
		public CultureInfo UiCulture => (CultureInfo) base["uiLanguage"];

		[ConfigurationCollection(typeof(Folder),
			AddItemName = "folder")]
		[ConfigurationProperty("folders")]
		public FolderCollection FoldersToListen => (FolderCollection) this["folders"];

		[ConfigurationCollection(typeof(AcceptanceRule),
			AddItemName = "rule")]
		[ConfigurationProperty("rules")]
		public AcceptanceRuleCollection AcceptanceRules => (AcceptanceRuleCollection) this["rules"];
	}
}
