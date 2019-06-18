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
			try
			{
				var gh = new WebsiteSaver.WebsiteProcessor();
				var uri = "https://www.google.com/";

				gh.GetPage(uri, @"C:\Users\Anastasiya_Trayanava\Documents\TestFolder");

				System.Console.ReadLine();
			}
			catch (Exception e)
			{
				System.Console.WriteLine(e);
			}
			//var folderToSaveTo = @"C:\Users\thesa\Documents\Proc";
			//var uri = "https://www.google.com/";

			//gh.DownloadWebsite(uri, folderToSaveTo);

			//System.Console.ReadLine();
		}
	}
}
