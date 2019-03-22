using System;
using TextProcess.Liba;

namespace TextProcess.Console.LibWrappers
{
	public static class TextProcessorCaller
	{
		public static void ReadAndGet()
		{
			try
			{
				var processor = new TextProcessor();

				System.Console.WriteLine("Specify file path:");
				var filePath = System.Console.ReadLine();

				var collection = processor.ReadTextAndReturnChars(filePath);

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
