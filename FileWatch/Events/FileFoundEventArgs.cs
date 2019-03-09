using System;
using FileWatch.Models;

namespace FileWatch.Events
{
	public class FileFoundEventArgs : EventArgs
	{
		public FileView CurrentUnit { get; set; }
	}
}
