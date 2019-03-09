using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileWatch.Events;
using FileWatch.Services;

namespace FileWatch.Tests
{
	public static class EventTestMethods
	{
		public static void Event_FileFound_Stop(object sender, FileFoundEventArgs e)
		{
			if (e.CurrentUnit.Extension.Equals(".mp3"))
			{
				if (sender is FileSystemWatch service)
					service.IsEndOfTheCollection = true;
			}
		}

		public static void Event_FileFound_Delete(object sender, FileFoundEventArgs e)
		{
			if (e.CurrentUnit.ParentFolder.Contains("OneMoreFolder"))
			{
				if (sender is FileSystemWatch service)
					service.IsForDelete = true;
			}
		}

		public static void Event_FilteredFileFound_Stop(object sender, FilteredFileFoundEventArgs e)
		{
			if (e.CurrentUnit.Extension.Equals(".mp3"))
			{
				if (sender is FileSystemWatch service)
					service.IsEndOfTheCollection = true;
			}
		}

		public static void Event_FilteredFileFound_Delete(object sender, FilteredFileFoundEventArgs e)
		{
			if (e.CurrentUnit.ParentFolder.Contains("OneMoreFolder"))
			{
				if (sender is FileSystemWatch service)
					service.IsForDelete = true;
			}
		}

		public static void Event_DirectoryFound_Stop(object sender, DirectoryFoundEventArgs e)
		{
			if (e.CurrentUnit.Name.Contains("Whosnext"))
			{
				if (sender is DirectorySystemWatch service)
					service.IsEndOfTheCollection = true;
			}
		}

		public static void Event_DirectoryFound_Delete(object sender, DirectoryFoundEventArgs e)
		{
			if (e.CurrentUnit.ParentFolder.Contains("OneMoreFolder"))
			{
				if (sender is DirectorySystemWatch service)
					service.IsForDelete = true;
			}
		}

		public static void Event_FilteredDirectoryFound_Delete(object sender, FilteredDirectoryFoundArgs e)
		{
			if (e.CurrentUnit.ParentFolder.Contains("OneMoreFolder"))
			{
				if (sender is DirectorySystemWatch service)
					service.IsForDelete = true;
			}
		}

		public static void Event_FilteredDirectoryFound_Stop(object sender, FilteredDirectoryFoundArgs e)
		{
			if (e.CurrentUnit.Name.Contains("Whosnext"))
			{
				if (sender is DirectorySystemWatch service)
					service.IsEndOfTheCollection = true;
			}
		}
	}
}