using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcMusicStore.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			LaunchLogParser();
		}

		static void LaunchLogParser()
		{
			try
			{
				System.Console.WriteLine("Creating a log journal...");

				var parser = new LogParserWrapper();
				var configFile = ConfigurationManager.AppSettings["LogsFilePath"] ?? string.Empty;

				parser.CreateJournal(configFile);
				System.Console.ReadLine();
			}
			catch (Exception e)
			{
				System.Console.WriteLine(e.Message);
			}
		}
	}
}
