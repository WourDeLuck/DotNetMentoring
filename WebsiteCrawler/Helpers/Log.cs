using System;

namespace WebsiteCrawler.Helpers
{
	public static class Log
	{
		public static void Error(string message)
		{
			Console.WriteLine($"Error: {message}");
		}

		public static void Info(string message)
		{
			Console.WriteLine($"Info: {message}");
		}
	}
}
