using System;

namespace MessageCreator
{
	public static class UserGreeting
	{
		public static string GreetUser(string name) => $"{DateTime.Now} Hello, {name}!";
	}
}
