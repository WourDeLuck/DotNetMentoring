using System.Configuration;

namespace FileSystemService.Common.Models
{
	public class AcceptanceRule : ConfigurationElement
	{
		[ConfigurationProperty("pattern", IsKey = true)]
		public string FileNamePattern { get; set; }

		[ConfigurationProperty("destination")]
		public Folder DestinationFolder { get; set; }

		[ConfigurationProperty("isAddingNumber")]
		public bool AddNumber { get; set; }

		[ConfigurationProperty("isAddingDate")]
		public bool AddMovementDate { get; set; }
	}
}
