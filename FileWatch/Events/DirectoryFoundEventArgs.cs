using System;
using FileWatch.Models;

namespace FileWatch.Events
{
	public class DirectoryFoundEventArgs : EventArgs
	{
		public DirectoryView CurrentUnit { get; set; }
	}
}
