using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileWatch.Events;
using FileWatch.Services;

namespace FileWatch.Console
{
	public class Program
	{
		static void Main(string[] args)
		{
			FileSystemWatch fs = new FileSystemWatch(new FileSystemWrapper(), y => !y.Name.Contains("5"));
			fs.Start += Event_Start;
			fs.Finish += Event_End;
			fs.FilteredFileFound += Event_DelteElement;

			var x = fs.CreateFileSequence(@"D:\Test");

			foreach (var i in x)
			{
				System.Console.WriteLine(i.Name);
			}
		}

		public static void Event_Start(object sender, EventArgs e)
		{
			System.Console.WriteLine("Search started");
		}

		public static void Event_End(object sender, EventArgs e)
		{
			System.Console.WriteLine("Search ended");
		}

		public static void Event_DelteElement(object sender, FilteredFileFoundEventArgs e)
		{
			if (e.CurrentUnit.Name.Contains("1"))
			{
				var app = sender as FileSystemWatch;
				app.IsForDelete = true;
				System.Console.WriteLine($"Deleted {e.CurrentUnit.Name}");
			}
		}
	}
}
