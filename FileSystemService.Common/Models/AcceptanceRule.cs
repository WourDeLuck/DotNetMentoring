using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemService.Common.Models
{
	public class AcceptanceRule
	{
		public string FileNamePattern { get; set; }

		public string DestinationFolder { get; set; }

		public bool AddNumber { get; set; }

		public bool AddMovementDate { get; set; }
	}
}
