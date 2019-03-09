using System;
using System.Linq;
using FileWatch.Interfaces;
using FileWatch.Models;
using FileWatch.Services;

namespace FileWatch.Console
{
	public class FileSystemMonitoringService
	{
		private ISystemFactory _fileFactory;
		private ISystemFactory _directoryFactory;

		public FileSystemMonitoringService(ISystemFactory fileFactory, ISystemFactory directoryFactory)
		{
			_fileFactory = fileFactory;
			_directoryFactory = directoryFactory;
		}

		public void WatchFiles(string startPoint, Func<FileView, bool> filterAlgorithm = null)
		{
			var fileSystemWatch = new FileSystemWatch(_fileFactory, filterAlgorithm);
			var methods = new EventMethods();

			fileSystemWatch.Start += methods.FileWatch_Start;
			fileSystemWatch.Finish += methods.FileWatch_Finish;
			fileSystemWatch.FileFound += methods.FileWatch_FileFound;
			fileSystemWatch.FilteredFileFound += methods.FileWatch_FilteredFileFound;

			var collection = fileSystemWatch.CreateFileSequence(startPoint).ToList();
		}

		public void WatchDirectories(string startPoint, Func<DirectoryView, bool> filterAlgorithm = null)
		{
			var directorySystemWatch = new DirectorySystemWatch(_directoryFactory, filterAlgorithm);
			var methods = new EventMethods();

			directorySystemWatch.Start += methods.DirectoryWatch_Start;
			directorySystemWatch.Finish += methods.DirectoryWatch_Finish;
			directorySystemWatch.DirectoryFound += methods.DirectoryWatch_FileFound;
			directorySystemWatch.FilteredDirectoryFound += methods.DirectoryWatch_FilteredFileFound;

			var collection = directorySystemWatch.CreateDirectorySequence(startPoint).ToList();
		}
	}
}
