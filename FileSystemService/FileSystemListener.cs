using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using FileSystemService.Common;
using FileSystemService.Common.Models;

namespace FileSystemService
{
    public class FileSystemListener
    {
	    private FileSystemWatcher _fileSystemWatcher;
	    private readonly IoTools _ioTools;
		private List<AcceptanceRule> _rules;

	    public FileSystemListener(List<AcceptanceRule> rulesForFiles)
	    {
		    _rules = rulesForFiles;
			_ioTools = new IoTools();
	    }

	    public void Initialize(string folderLink)
	    {
		    Log.Info("Initialize File System listener");
			Guard.ThrowDirectoryExistence(folderLink);

		    _rules = new List<AcceptanceRule>();
		    _fileSystemWatcher = new FileSystemWatcher(folderLink);
		    _fileSystemWatcher.Changed += OnWatcherChanged;
		    _fileSystemWatcher.EnableRaisingEvents = true;
		    _fileSystemWatcher.IncludeSubdirectories = true;

	    }

	    private void OnWatcherChanged(object sender, FileSystemEventArgs e)
	    {
		    Log.Info($"New file has been found: {e.Name}");

		    //foreach (var rule in _rules)
		    //{
			   // if (!Regex.IsMatch(e.Name, rule.FileNamePattern)) continue;

			   // Log.Info($"Rule has been found that matches this file: {rule.FileNamePattern}");
			   // _ioTools.MoveFile(e.FullPath, rule.DestinationFolder);
		    //}

		    var rule = _rules.FirstOrDefault(r => Regex.IsMatch(e.Name, r.FileNamePattern));

		    if (rule == null) return;

		    Log.Info($"Rule has been found that matches this file: {rule.FileNamePattern}");

		    var newPath = _ioTools.MoveFile(e.FullPath, rule.DestinationFolder.Path);
		    var fileWithDate = rule.AddMovementDate ? _ioTools.RenameFile(newPath, $"{DateTime.Now}-{e.Name}") : e.Name;
	    }
    }
}
