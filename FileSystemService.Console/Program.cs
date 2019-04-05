using System;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FileSystemService.Common.Models;

namespace FileSystemService.Console
{
	public class Program
	{
		static void Main(string[] args)
		{
			InitService();
		}

		private static async Task InitService()
		{
			var config = GetConfiguration();
			var rules = config.AcceptanceRules.Cast<AcceptanceRule>().ToList();
			var folders = config.FoldersToListen.Cast<Folder>().ToList();
			var defaultFolder = config.DefaultFolder;
			Thread.CurrentThread.CurrentUICulture = config.UiCulture;

			var listener = new FileSystemListener(rules, defaultFolder);

			foreach (var folder in folders)
			{
				try
				{
					await listener.Initialize(folder.Path);
				}
				catch (Exception e)
				{
					System.Console.WriteLine(e.Message);
				}
			}

			while (true)
			{
				var key = System.Console.ReadLine();
				if (!string.IsNullOrEmpty(key) && key.Equals("exit"))
				{
					Environment.Exit(0);
				}
			}
		}

		private static StartSettings GetConfiguration() => (StartSettings)ConfigurationManager.GetSection("startSettings");
	}
}
