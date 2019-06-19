using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RequestHandler.Models;

namespace RequestHandler.Services
{
	public class DataAccess
	{
		public void Test()
		{
			var db = new NorthwindEntities();
		}
	}
}