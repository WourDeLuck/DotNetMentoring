using System;
using System.Collections.Generic;
using System.Linq;
using FileWatch.Events;
using FileWatch.Interfaces;
using FileWatch.Models;

namespace FileWatch.Services
{
	/// <summary>
	/// Watch class for files.
	/// </summary>
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

		/// <summary>
		/// Public constructor for the class.
		/// </summary>
		/// <param name="fileWrapper">Factory class.</param>
		/// <param name="filterAlgorithm">Algorithm for additional sorting.</param>
		public FileSystemWatch(ISystemFactory fileWrapper, Func<FileView, bool> filterAlgorithm = null)
		{
			_factory = fileWrapper;
			_algoritm = filterAlgorithm;
		}

		/// <summary>
		/// Gets all files from a start point and filters it according to user-specified instructions.
		/// </summary>
		/// <param name="path">Start point.</param>
		/// <returns>Collection of files.</returns>
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
		
		/// <summary>
		/// Gets files from a start point.
		/// </summary>
		/// <param name="path">Start point.</param>
		/// <returns>Collection of files.</returns>
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

		/// <summary>
		/// Filters collection according to additional instructions.
		/// </summary>
		/// <param name="files">Collection to filter.</param>
		/// <returns>Filtered collection of files.</returns>
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

		/// <summary>
		/// Event Start invocation.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnSearchStart(EventArgs e)
		{
			Start?.Invoke(this, e);
		}

		/// <summary>
		/// Event Finish invocation.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnSearchEnd(EventArgs e)
		{
			Finish?.Invoke(this, e);
		}

		/// <summary>
		/// Event FileFoundInvocation.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnFileFound(FileFoundEventArgs e)
		{
			FileFound?.Invoke(this, e);
		}

		/// <summary>
		/// Event FilteredFileFound invocation.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnFilteredFileFound(FilteredFileFoundEventArgs e)
		{
			FilteredFileFound?.Invoke(this, e);
		}
	}
}