using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RequestHandler.Helpers
{
	public static class Guard
	{
		public static void ValidateString(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				throw new ArgumentNullException(str, "Customer ID is empty.");
			}
		}
	}
}