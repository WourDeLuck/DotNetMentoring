using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using FileSystemService.Common.Helpers;
using FileSystemService.Common.Models;
using logging = FileSystemService.Common.Resources.Logging;

namespace FileSystemService
{
	public class FileSystemListener
	{
		private FileSystemWatcher _fileSystemWatcher;
		private readonly IoTools _ioTools;
		private readonly List<AcceptanceRule> _rules;
		private readonly CultureInfo _cultureInfo = CultureInfo.CurrentUICulture;

		public FileSystemListener(List<AcceptanceRule> rulesForFiles)
		{
			_rules = rulesForFiles;
			_ioTools = new IoTools();
		}

		public void Initialize(string folderLink)
		{
			Log.Info(logging.InitListener);
			Guard.ThrowDirectoryExistence(folderLink);

			_fileSystemWatcher = new FileSystemWatcher(folderLink);
			_fileSystemWatcher.Changed += OnWatcherChanged;
			_fileSystemWatcher.EnableRaisingEvents = true;
		}

		private void OnWatcherChanged(object sender, FileSystemEventArgs e)
		{
			if (!File.Exists(e.FullPath))
			{
				return;
			}
			Log.Info($"{logging.FileFound}: {e.Name}");

			var rule = _rules.FirstOrDefault(r => Regex.IsMatch(e.Name, r.FileNamePattern));

			if (rule == null)
			{
				Log.Info(logging.RuleNotFound);
				return;
			}

			Log.Info($"{logging.RuleFound}: {rule.FileNamePattern}");

			var newPath = _ioTools.MoveFile(e.FullPath, rule.DestinationFolder);

			if (rule.AddMovementDate)
			{
				var dateTime = DateTime.Now.ToString(_cultureInfo.DateTimeFormat);
				newPath = _ioTools.RenameFile(newPath, $"{dateTime.EscapeDateTimeSymbols()} {Path.GetFileName(newPath)}");
			}

			if (rule.AddNumber)
			{
				var fileCount = _ioTools.GetFileCount(newPath);
				_ioTools.RenameFile(newPath, $"{fileCount + 1}. {Path.GetFileName(newPath)}");
			}
		}
	}
}