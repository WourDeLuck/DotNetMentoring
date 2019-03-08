using System;
using System.Collections.Generic;
using FileWatch.Models;

namespace FileWatch.Events
{
	public class FilteredDirectoryFoundArgs : EventArgs
	{
		public DirectoryView CurrentUnit { get; set; }

		public IEnumerable<DirectoryView> FullCollection { get; set; }
	}
}
