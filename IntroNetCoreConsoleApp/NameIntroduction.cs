using System;
using System.Collections.Generic;
using MessageCreator;
using Microsoft.Extensions.Configuration;

namespace IntroNetCoreConsoleApp
{
	/// <summary>
	/// Class for the task 3.
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

			Console.WriteLine(UserGreeting.GreetUser(name));
		}
	}
}