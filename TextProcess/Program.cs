using System;

namespace TextProcess
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

				Console.WriteLine("Enter some text:");
				var originalString = Console.ReadLine();

				Console.WriteLine($"First symbol: {textProcesser.GetFirstLetterOfString(originalString)}");
			}
			catch (Exception e) when (e is ArgumentNullException || e is ArgumentException)
			{
				Console.WriteLine(e.Message);
			}
			catch (Exception e)
			{
				Console.WriteLine($"Something went wrong: {e.Message}");
			}
		}
	}
}