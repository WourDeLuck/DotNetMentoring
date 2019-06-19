using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
