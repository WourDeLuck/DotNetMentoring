using System;
using TextProcess.Arithmetics;
using TextProcess.Console.Enums;
using TextProcess.Console.Menu;

namespace TextProcess.Console.LibWrappers
{
	public static class StringConverterCaller
	{
		public static void ConvertToNumber(NumberTypeEnum selectedType)
		{
			try
			{
				System.Console.WriteLine("Enter a string:");
				var stringToConvert = System.Console.ReadLine();

				switch (selectedType)
				{
					case NumberTypeEnum.Int:
						var intValue = stringToConvert.ConvertToInt();
						System.Console.WriteLine(
							$"Convertation to int has been completed successfully. Converted value: {intValue}. Variable type: {intValue.GetType()}");
						break;
					case NumberTypeEnum.Long:
						var longValue = stringToConvert.ConvertToLong();
						System.Console.WriteLine(
							$"Convertation to long has been completed successfully. Converted value: {longValue}. Variable type: {longValue.GetType()}");
						break;
				}
			}
			catch (Exception e)
			{
				System.Console.WriteLine($"Something went wrong: {e.Message}");
			}
			finally
			{
				MenuCreator.ConvertToNumberMenu();
			}
		}
	}
}