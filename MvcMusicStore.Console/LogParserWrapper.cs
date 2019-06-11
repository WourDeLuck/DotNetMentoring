using System;
using System.Collections.Generic;
using MSUtil;
using System.IO;
using System.Linq;
using MvcMusicStore.Console.Enums;
using MvcMusicStore.Console.Helpers;

namespace MvcMusicStore.Console
{
	public class LogParserWrapper
	{
		public void CreateJournal(string logFilePath)
		{
			if (!File.Exists(logFilePath))
			{
				throw new FileNotFoundException($"Specified file not found: {logFilePath}");
			}

			PrepareFile(logFilePath);

			// list of errors
			GetListOfErrors(logFilePath);

			// amount of items in each level
			GetAmountOfItemsInEachLevel(logFilePath);
		}

		private void GetListOfErrors(string logFilePath)
		{
			var levelName = LogLevels.Error.GetDisplayName();
			var errorListQuery = $"SELECT * FROM {logFilePath} WHERE Level='{levelName}'";

			var results = GetLogs(errorListQuery);

			System.Console.WriteLine($"Logs of level {levelName}:");
			while (!results.atEnd())
			{
				System.Console.WriteLine($"{results.getRecord().getValue("DateTime")} {results.getRecord().getValue("Message")}");
				results.moveNext();
			}
		}


		private void GetAmountOfItemsInEachLevel(string logFilePath)
		{
			var query = $"SELECT COUNT(*), Level FROM {logFilePath} GROUP BY Level";
			var results = GetLogs(query);

			var listOfItems = new List<string>();

			while (!results.atEnd())
			{
				listOfItems.Add($"{results.getRecord().getValue("Level")} {results.getRecord().getValue("COUNT(ALL *)")}");
				results.moveNext();
			}

			PutAdditionalInfo(listOfItems);

			System.Console.WriteLine("Amount of logs of each level:");
			foreach (var item in listOfItems)
			{
				System.Console.WriteLine(item);
			}
		}

		private void PutAdditionalInfo(List<string> listOfItems)
		{
			var levels = Enum.GetValues(typeof(LogLevels)).Cast<LogLevels>().Select(x => x.GetDisplayName()).ToList();

			foreach (var level in levels)
			{
				if (!listOfItems.Any(x => x.Contains(level)))
				{
					listOfItems.Add($"{level} 0");
				}
			}
		}

		private ILogRecordset GetLogs(string query)
		{
			LogQueryClass logQuery = new LogQueryClassClass();
			var inputFile = new COMXMLInputContextClassClass();

			return logQuery.Execute(query, inputFile);
		}

		/// <summary>
		/// Temporary solution for log4net not writing the root tag for xml-styled logs. Without the root tag, LogParser won't read the logs. Looking into writing custom file appender
		/// </summary>
		/// <param name="filePath"></param>
		private void PrepareFile(string filePath)
		{
			var fileContent = File.ReadAllText(filePath);
			var header = "<LogEntries>";
			var footer = "</LogEntries>";
			string newContent = "";

			if (!fileContent.Contains(header) && !fileContent.Contains(footer))
			{
				newContent = $"{header}{fileContent}{footer}";
			}
			else if (fileContent.Contains(header) && fileContent.Contains(footer))
			{
				var trimed = fileContent.Replace(footer, "");
				newContent = $"{trimed}{footer}";
			}

			File.WriteAllText(filePath, newContent);
		}
	}
}