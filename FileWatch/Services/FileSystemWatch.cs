using System;
using System.Collections.Generic;
using System.Linq;
using FileWatch.Events;
using FileWatch.Interfaces;
using FileWatch.Models;

namespace FileWatch.Services
{
	public class FileSystemVisitor
	{
		private Func<FileView, bool> _algoritm;
		private IFileSystemWrapper _fileWrapper;

		public bool IsForDelete { get; set; }

		public bool IsEndOfTheCollection { get; set; }

		public event EventHandler Start;
		public event EventHandler Finish;
		public event EventHandler<FileFoundEventArgs> FileFound;
		public event EventHandler<FilteredFileFoundEventArgs> FilteredFileFound;

		public FileSystemVisitor(IFileSystemWrapper fileWrapper, Func<FileView, bool> filterAlgorithm = null)
		{
			_fileWrapper = fileWrapper;
			_algoritm = filterAlgorithm;
		}

		public IEnumerable<FileView> CreateFileSequence(string path)
		{
			OnSearchStart(EventArgs.Empty);

			var files = GetContent(path);
			var filteredContent = FilterContent(files);

			OnSearchEnd(EventArgs.Empty);
			return filteredContent;
		}

		public IEnumerable<FileView> GetContent(string path)
		{
			foreach (var file in _fileWrapper.GetFiles(path))
			{
				var fileModel = new FileView(file);
				yield return fileModel;
;
				OnFileFound(new FileFoundEventArgs
				{
					CurrentUnit = fileModel
				});
			}

			foreach (var folder in _fileWrapper.GetDirectories(path))
			{
				foreach (var file in GetContent(folder))
				{
					yield return file;
					OnFileFound(new FileFoundEventArgs
					{
						CurrentUnit = file
					});
				}
			}
		}

		public IEnumerable<FileView> FilterContent(IEnumerable<FileView> files)
		{
			var filteredCollection = files.Where(_algoritm);

			foreach (var unit in filteredCollection)
			{
				yield return unit;
				OnFilteredFileFound(new FilteredFileFoundEventArgs
				{
					CurrentUnit = unit,
					FullCollection = filteredCollection
				});
			}
		}

		protected virtual void OnSearchStart(EventArgs e)
		{
			Start?.Invoke(this, e);
		}

		protected virtual void OnSearchEnd(EventArgs e)
		{
			Finish?.Invoke(this, e);
		}

		protected virtual void OnFileFound(FileFoundEventArgs e)
		{
			FileFound?.Invoke(this, e);
		}

		protected virtual void OnFilteredFileFound(FilteredFileFoundEventArgs e)
		{
			FilteredFileFound?.Invoke(this, e);
		}
	}
}