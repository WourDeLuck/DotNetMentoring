using System;

namespace TextProcess.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{
				ReadAndGet();
			}
		}

		public static void ReadAndGet()
		{
			try
			{
				var textProcesser = new TextProcesser();

				System.Console.WriteLine("Specify file path:");
				var originalString = System.Console.ReadLine();

				System.Console.WriteLine($"First symbol: {textProcesser.GetFirstLetterOfString(originalString)}");
			}
			catch (Exception e) when (e is ArgumentNullException || e is ArgumentException)
			{
				System.Console.WriteLine(e.Message);
			}
			catch (Exception e)
			{
				System.Console.WriteLine($"Something went wrong: {e.Message}");
			}
		}
	}
}
