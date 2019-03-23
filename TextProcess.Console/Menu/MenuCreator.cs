using System;
using TextProcess.Console.Enums;
using TextProcess.Console.LibWrappers;

namespace TextProcess.Console.Menu
{
	public static class MenuCreator
	{
		public static void CommonMenu()
		{
			System.Console.WriteLine("1. Get first letter of each line in text");
			System.Console.WriteLine("2. Convert string to a number");
			System.Console.WriteLine("3. Exit");

			var success = int.TryParse(System.Console.ReadLine(), out var value);

			if (!success)
			{
				System.Console.WriteLine("Enter a correct value.");
			}
			else
			{
				switch (value)
				{
					case 1:
						TextProcessorCaller.ReadAndGet();
						break;
					case 2:
						ConvertToNumberMenu();
						break;
					case 3:
						Environment.Exit(0);
						break;
					default:
						System.Console.WriteLine("Enter a correct value.");
						break;
				}
			}
		}

		public static void ConvertToNumberMenu()
		{
			System.Console.WriteLine("Convert to number:");
			System.Console.WriteLine("1. Convert to int");
			System.Console.WriteLine("2. Convert to long");
			System.Console.WriteLine("3. Back");

			var success = int.TryParse(System.Console.ReadLine(), out var value);

			if (!success)
			{
				System.Console.WriteLine("Enter a correct value.");
			}
			else
			{
				switch (value)
				{
					case 1:
						StringConverterCaller.ConvertToNumber(NumberTypeEnum.Int);
						break;
					case 2:
						StringConverterCaller.ConvertToNumber(NumberTypeEnum.Long);
						break;
					case 3:
						System.Console.Clear();
						CommonMenu();
						break;
					default:
						System.Console.WriteLine("Enter a correct value.");
						break;
				}
			}
		}
	}
}