using System;

namespace IntroNetCoreConsoleApp
{
	public class NameIntroduction
	{
		static void Main(string[] args)
		{
			Console.WriteLine(SayHelloToUser(string.Join(", ", args)));
		}

		private static string SayHelloToUser(string name) => $"Hello, {name}!";
	}
}