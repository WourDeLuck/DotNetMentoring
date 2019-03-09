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
		private ISystemFactory _factory;

		public bool IsForDelete { get; set; }

		public bool IsEndOfTheCollection { get; set; }

		public event EventHandler Start;
		public event EventHandler Finish;
		public event EventHandler<DirectoryFoundEventArgs> DirectoryFound;
		public event EventHandler<FilteredDirectoryFoundArgs> FilteredDirectoryFound;

		public DirectorySystemWatch(ISystemFactory fileSystemWrapper, Func<DirectoryView, bool> filterAlgorithm = null)
		{
			_factory = fileSystemWrapper;
			_algoritm = filterAlgorithm;
		}

		public IEnumerable<DirectoryView> CreateFileSequence(string path)
		{
			OnStart();

			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException(path);
			}

			var directories = GetContent(path);
			var filterStage = _algoritm != null ? FilterContent(directories) : directories;

			OnFinish();
			return filterStage;
		}

		private IEnumerable<DirectoryView> GetContent(string path)
		{
			foreach (var folder in _factory.GetFileSystemContent(path))
			{
				var directoryModel = new DirectoryView(folder);
				
				OnDirectoryFound(new DirectoryFoundEventArgs
				{
					CurrentUnit = directoryModel
				});

				if (!IsForDelete)
				{
					yield return directoryModel;
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

		private IEnumerable<DirectoryView> FilterContent(IEnumerable<DirectoryView> files)
		{
			if (files == null)
			{
				throw new ArgumentException("Cannot perform filtering on an empty collection.");
			}

			var filteredCollection = files.Where(_algoritm);

			foreach (var unit in filteredCollection)
			{
				OnFilteredDirectoryFound(new FilteredDirectoryFoundArgs
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