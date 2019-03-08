using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileWatch.Models;

namespace FileWatch.Events
{
	public class FilteredFileFoundEventArgs : EventArgs
	{
		public FileView CurrentUnit { get; set; }

		public IEnumerable<FileView> FullCollection { get; set; }
	}
}
