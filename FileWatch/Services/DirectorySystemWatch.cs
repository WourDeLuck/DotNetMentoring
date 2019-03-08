using System;
using System.Collections.Generic;
using System.Linq;
using FileWatch.Events;
using FileWatch.Interfaces;
using FileWatch.Models;

namespace FileWatch.Services
{
	public class DirectorySystemWatch
	{
		private Func<DirectoryView, bool> _algoritm;
		private IFileSystemWrapper _fileWrapper;

		public bool IsForDelete { get; set; }

		public bool IsEndOfTheCollection { get; set; }

		public event EventHandler Start;
		public event EventHandler Finish;
		public event EventHandler<DirectoryFoundEventArgs> DirectoryFound;
		public event EventHandler<FilteredDirectoryFoundArgs> FilteredDirectoryFound;

		public DirectorySystemWatch(IFileSystemWrapper fileSystemWrapper, Func<DirectoryView, bool> filterAlgorithm = null)
		{
			_fileWrapper = fileSystemWrapper;
			_algoritm = filterAlgorithm;
		}

		public IEnumerable<DirectoryView> CreateFileSequence(string path)
		{
			OnStart();

			var files = GetContent(path);
			var filteredContent = FilterContent(files);

			OnFinish();
			return filteredContent;
		}

		public IEnumerable<DirectoryView> GetContent(string path)
		{
			foreach (var folder in _fileWrapper.GetDirectories(path))
			{
				var fileModel = new DirectoryView(folder);
				yield return fileModel;
				
				OnDirectoryFound(new DirectoryFoundEventArgs
				{
					CurrentUnit = fileModel
				});

				foreach (var innerFolder in GetContent(folder))
				{
					yield return innerFolder;
					OnDirectoryFound(new DirectoryFoundEventArgs
					{
						CurrentUnit = fileModel
					});
				}
			}
		}

		public IEnumerable<DirectoryView> FilterContent(IEnumerable<DirectoryView> files)
		{
			var filteredCollection = files.Where(_algoritm);

			foreach (var unit in filteredCollection)
			{
				yield return unit;
				OnFilteredDirectoryFound(new FilteredDirectoryFoundArgs
				{
					CurrentUnit = unit,
					FullCollection = filteredCollection
				});
			}
		}

		protected virtual void OnStart()
		{
			Start?.Invoke(this, EventArgs.Empty);
		}

		protected virtual void OnFinish()
		{
			Finish?.Invoke(this, EventArgs.Empty);
		}

		protected virtual void OnDirectoryFound(DirectoryFoundEventArgs e)
		{
			DirectoryFound?.Invoke(this, e);
		}

		protected virtual void OnFilteredDirectoryFound(FilteredDirectoryFoundArgs e)
		{
			FilteredDirectoryFound?.Invoke(this, e);
		}
	}
}