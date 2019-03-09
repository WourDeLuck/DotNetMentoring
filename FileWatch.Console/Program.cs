using System.Collections.Generic;
using System.Xml.Schema;
using FileWatch.Services;
using Microsoft.Extensions.Configuration;

namespace FileWatch.Console
{
	public class Program
	{
		static void Main(string[] args)
		{
			BindArguments(args);
		}

		public static void BindArguments(string[] args)
		{
			var builder = new ConfigurationBuilder();
			builder.AddCommandLine(args, new Dictionary<string, string>
			{
				["-Path"] = "Path"
			});

			var config = builder.Build();
			var path = config["Path"];

			InvokeFileSystemWatcher(path);
		}

		public static void InvokeFileSystemWatcher(string startPoint)
		{
			if (string.IsNullOrEmpty(startPoint)) return;
			var fileSystemMonitoring = new FileSystemMonitoringService(new FileSystemFactory(), new DirectorySystemFactory());

			fileSystemMonitoring.WatchFiles(startPoint);
			fileSystemMonitoring.WatchDirectories(startPoint);
		}
	}
}
