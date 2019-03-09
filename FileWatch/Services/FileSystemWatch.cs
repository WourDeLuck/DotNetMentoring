using System;
using System.Collections.Generic;
using System.Linq;
using FileWatch.Events;
using FileWatch.Interfaces;
using FileWatch.Models;

namespace FileWatch.Services
{
	public class FileSystemWatch
	{
		private Func<FileView, bool> _algoritm;
		private ISystemFactory _factory;

		public bool IsForDelete { get; set; }

		public bool IsEndOfTheCollection { get; set; }

		public event EventHandler Start;
		public event EventHandler Finish;
		public event EventHandler<FileFoundEventArgs> FileFound;
		public event EventHandler<FilteredFileFoundEventArgs> FilteredFileFound;

		public FileSystemWatch(ISystemFactory fileWrapper, Func<FileView, bool> filterAlgorithm = null)
		{
			_factory = fileWrapper;
			_algoritm = filterAlgorithm;
		}

		public IEnumerable<FileView> CreateFileSequence(string path)
		{
			OnSearchStart(EventArgs.Empty);

			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException(path);
			}

			var files = GetContent(path);
			var filterStage = _algoritm != null ? FilterContent(files) : files;

			OnSearchEnd(EventArgs.Empty);
			return filterStage;
		}

		private IEnumerable<FileView> GetContent(string path)
		{
			foreach (var file in _factory.GetFileSystemContent(path))
			{
				var fileModel = new FileView(file);

				OnFileFound(new FileFoundEventArgs
				{
					CurrentUnit = fileModel
				});

				if (!IsForDelete)
				{
					yield return fileModel;
				}
				else
				{
					IsForDelete = false;
				}

				if (IsEndOfTheCollection)
				{
					IsEndOfTheCollection = false;
					break;
				}
			}
		}

		private IEnumerable<FileView> FilterContent(IEnumerable<FileView> files)
		{
			if (files == null)
			{
				throw new ArgumentException("Cannot perform filtering on an empty collection.");
			}

			var filteredCollection = files.Where(_algoritm);

			foreach (var unit in filteredCollection)
			{
				OnFilteredFileFound(new FilteredFileFoundEventArgs
				{
					CurrentUnit = unit,
					FullCollection = filteredCollection
				});

				if (!IsForDelete)
				{
					yield return unit;
				}

				if (!IsEndOfTheCollection) continue;
				IsEndOfTheCollection = false;
				break;
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