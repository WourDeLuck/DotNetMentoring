using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileWatch.Events;

namespace FileWatch.Console
{
	public class EventMethods
	{
		public virtual void FileWatch_Start(object sender, EventArgs e)
		{
			System.Console.WriteLine("File watching started");
		}

		public virtual void FileWatch_Finish(object sender, EventArgs e)
		{
			System.Console.WriteLine("File watching finished");
		}

		public virtual void FileWatch_FileFound(object sender, FileFoundEventArgs e)
		{
			System.Console.WriteLine($"Found file: {e.CurrentUnit.Name}");
		}

		public virtual void FileWatch_FilteredFileFound(object sender, FilteredFileFoundEventArgs e)
		{
			System.Console.WriteLine($"Found file that passed filtering: {e.CurrentUnit.Name}");
		}

		public virtual void DirectoryWatch_Start(object sender, EventArgs e)
		{
			System.Console.WriteLine("Directory watching started");
		}

		public virtual void DirectoryWatch_Finish(object sender, EventArgs e)
		{
			System.Console.WriteLine("Directory watching finished");
		}

		public virtual void DirectoryWatch_FileFound(object sender, DirectoryFoundEventArgs e)
		{
			System.Console.WriteLine($"Found directory: {e.CurrentUnit.Name}");
		}

		public virtual void DirectoryWatch_FilteredFileFound(object sender, FilteredDirectoryFoundArgs e)
		{
			System.Console.WriteLine($"Found directory that passed filtering: {e.CurrentUnit.Name}");
		}
	}
}
