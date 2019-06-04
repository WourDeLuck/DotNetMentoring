using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MvcMusicStore.Console.Helpers
{
	public static class EnumHelper
	{
		public static string GetDisplayName(this Enum enumType)
		{
			return enumType.GetType().GetMember(enumType.ToString())
				.First()
				.GetCustomAttribute<DisplayAttribute>()
				.Name;
		}
	}
}
