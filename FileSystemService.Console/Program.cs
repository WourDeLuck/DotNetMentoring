using System;
using System.Configuration;
using System.Linq;
using FileSystemService.Common.Models;

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
			var rules = config.AcceptanceRules.Cast<AcceptanceRule>().ToList();
			var folders = config.FoldersToListen.Cast<Folder>().ToList();

			var listener = new FileSystemListener(rules);
			listener.Initialize(folders.Select(x => x.Path).FirstOrDefault());

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
