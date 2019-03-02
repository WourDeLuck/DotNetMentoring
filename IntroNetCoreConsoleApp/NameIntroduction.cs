using System;
using System.Collections.Generic;
using MessageCreator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.CommandLine;

namespace IntroNetCoreConsoleApp
{
	public class NameIntroduction
	{
		static void Main(string[] args)
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

		private static string SayHelloToUser(string name) => $"Hello, {name}!";
	}
}