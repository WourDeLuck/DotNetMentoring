using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using FileSystemService.Common;
using FileSystemService.Common.Models;

namespace FileSystemService
{
    public class FileSystemListener
    {
	    private FileSystemWatcher _fileSystemWatcher;

	    public List<AcceptanceRule> Rules;

	    public void Initialize(string folderLink)
	    {
		    Log.Info("Initialize File System listener");
			CheckFolderExists(folderLink);

			Rules = new List<AcceptanceRule>();
		    _fileSystemWatcher = new FileSystemWatcher(folderLink);
		    _fileSystemWatcher.Changed += OnWatcherChanged;
		    _fileSystemWatcher.EnableRaisingEvents = true;
		    _fileSystemWatcher.IncludeSubdirectories = true;

	    }

	    private void CheckFolderExists(string folderLink)
	    {
		    if (!Directory.Exists(folderLink))
		    {
			    throw new DirectoryNotFoundException($"Specified directory wasn't found: {folderLink}");
		    }
	    }

	    private void OnWatcherChanged(object sender, FileSystemEventArgs e)
	    {
		    Log.Info($"New file has been found: {e.Name}");

		    foreach (var rule in Rules)
		    {
			    if (Regex.IsMatch(e.Name, rule.FileNamePattern))
			    {
				    IoTools io = new IoTools();
					io.MoveFile(e.FullPath, rule.DestinationFolder);
			    }
		    }
	    }
    }
}
