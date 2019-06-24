using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCrawler.Services;

namespace WebsiteCrawler.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				var gh = new WebsiteProcessor();
				var uri = "https://www.google.com/";

				gh.GetPage(uri, @"D:\KK");

				System.Console.ReadLine();
			}
			catch (Exception e)
			{
				System.Console.WriteLine(e);
			}
		}
	}
}
