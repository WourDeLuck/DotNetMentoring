using System.Configuration;

namespace FileSystemService.Common.Models
{
	public class AcceptanceRuleCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new AcceptanceRule();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((AcceptanceRule) element).FileNamePattern;
		}
	}
}
