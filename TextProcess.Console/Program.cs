using System;
using TextProcess.Liba;

namespace TextProcess.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			ReadAndGet();
		}

		public static void ReadAndGet()
		{
			try
			{
				var textProcesser = new TextProcesser();

				System.Console.WriteLine("Specify file path:");
				var filePath = System.Console.ReadLine();

				var collection = textProcesser.ReadTextAndReturnChars(filePath);

				foreach (var item in collection)
				{
					System.Console.WriteLine($"First symbol: {item}");
				}
			}
			catch (Exception e)
			{
				System.Console.WriteLine($"Something went wrong: {e.Message}");
			}
		}
	}
}
