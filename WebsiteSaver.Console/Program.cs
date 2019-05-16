using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteSaver.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			var gh = new WebsiteProcessor();

			var folderToSaveTo = @"C:\Users\thesa\Documents\Proc";
			var uri = "https://www.google.com/";

			gh.DownloadWebsite(uri, folderToSaveTo, 0);

			System.Console.ReadLine();
		}
	}
}
