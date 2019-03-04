using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace IntroNetCoreConsoleApp.First
{
	/// <summary>
	/// Class for the task 1.
	/// </summary>
	public class NameIntroduction
	{
		static void Main(string[] args)
		{
			BindArguments(args);
		}

		private static void BindArguments(string[] args)
		{
			var builder = new ConfigurationBuilder();
			builder.AddCommandLine(args, new Dictionary<string, string>
			{
				["-Name"] = "Name"
			});

			var config = builder.Build();
			var name = config["Name"];

			Console.WriteLine(SayHelloToUser(name));
		}

		private static string SayHelloToUser(string name) => $"Hello, {name}!";
	}
}