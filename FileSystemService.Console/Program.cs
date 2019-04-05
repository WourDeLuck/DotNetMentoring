using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileSystemService.Common.Models;

namespace FileSystemService.Console
{
	public class Program
	{
		static void Main(string[] args)
		{
		}

		public static StartSettings GetConfiguration() => (StartSettings)ConfigurationManager.GetSection("startSettings");
	}
}
