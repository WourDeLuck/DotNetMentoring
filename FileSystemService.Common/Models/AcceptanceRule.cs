using System.Configuration;

namespace FileSystemService.Common.Models
{
	public class AcceptanceRule : ConfigurationElement
	{
		[ConfigurationProperty("pattern", IsKey = true)]
		public string FileNamePattern => (string)base["pattern"];

		[ConfigurationProperty("destination")]
		public string DestinationFolder => (string)base["destination"];

		[ConfigurationProperty("isAddingNumber")]
		public bool AddNumber => (bool)base["isAddingNumber"];

		[ConfigurationProperty("isAddingDate")]
		public bool AddMovementDate => (bool)base["isAddingDate"];
	}
}
