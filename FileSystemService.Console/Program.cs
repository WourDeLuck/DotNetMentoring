using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FileSystemService.Common.Models;
using messages = FileSystemService.Common.Resources.ConsoleMessages;

namespace FileSystemService.Console
{
	public class Program
	{
		static void Main(string[] args)
		{
			InitService();
		}

		private static void InitService()
		{
			var config = GetConfiguration();

			CheckConfiguration(config);

			var rules = config.AcceptanceRules.Cast<AcceptanceRule>().ToList();
			var folders = config.FoldersToListen.Cast<Folder>().ToList();
			var defaultFolder = config.DefaultFolder;
			Thread.CurrentThread.CurrentUICulture = config.UiCulture;

			var listener = new FileSystemListener(rules, defaultFolder);

			var taskArray = folders.Select(folder => Task.Factory.StartNew(() => listener.Initialize(folder.Path))).ToList();

			System.Console.WriteLine($@"{messages.FolderWatch}: {taskArray.Count}");

			while (true)
			{
				var key = System.Console.ReadLine();
				if (!string.IsNullOrEmpty(key) && key.Equals("exit"))
				{
					Environment.Exit(0);
				}
			}
		}

		private static void CheckConfiguration(StartSettings config)
		{
			if (config == null)
			{
				System.Console.WriteLine(messages.EmptyConfig);
				Environment.Exit(0);
			}

			if (config.UiCulture == null)
			{
				System.Console.WriteLine(messages.NullCulture);
				Environment.Exit(0);
			}

			var rules = config.AcceptanceRules.Cast<AcceptanceRule>().ToList();

			if (rules.Any())
			{
				foreach (var rule in rules)
				{
					if (rule.DestinationFolder != null && rule.FileNamePattern != null) continue;
					System.Console.WriteLine(messages.NullFolderOrPattern);
					Environment.Exit(0);
				}
			}

			var folders = config.FoldersToListen.Cast<Folder>().ToList();

			if (folders.Any())
			{
				foreach (var folder in folders)
				{
					if (folder.Path != null) continue;
					System.Console.WriteLine(messages.NullFolderPath);
					Environment.Exit(0);
				}
			}

			var defaultFolder = config.DefaultFolder;

			if (defaultFolder == null)
			{
				System.Console.WriteLine(messages.NullDefaultFolder);
				Environment.Exit(0);
			}
		}

		private static StartSettings GetConfiguration() => (StartSettings)ConfigurationManager.GetSection("startSettings");
	}
}
