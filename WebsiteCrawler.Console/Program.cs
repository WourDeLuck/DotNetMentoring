using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCrawler.Helpers;
using WebsiteCrawler.Services;

namespace WebsiteCrawler.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			StartWebsiteCrawler();
		}

		private static void StartWebsiteCrawler()
		{
			try
			{
				var webCrawler = new WebsiteProcessor();
				var uri = "https://www.google.com/";

				webCrawler.DownloadWebsite(uri, @"D:\KK", 1);

				System.Console.ReadLine();
			}
			catch (Exception e)
			{
				Log.Error(e.Message);
				System.Console.WriteLine();
			}
		}
	}
}
