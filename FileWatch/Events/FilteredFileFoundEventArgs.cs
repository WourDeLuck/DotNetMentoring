using System;
using System.Collections.Generic;
using FileWatch.Models;

namespace FileWatch.Events
{
	public class FilteredFileFoundEventArgs : EventArgs
	{
		public FileView CurrentUnit { get; set; }

		public IEnumerable<FileView> FullCollection { get; set; }
	}
}
