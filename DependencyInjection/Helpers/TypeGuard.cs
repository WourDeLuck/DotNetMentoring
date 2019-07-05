using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DependencyInjection.Attributes;

namespace DependencyInjection.Helpers
{
	public static class TypeGuard
	{
		public static void CheckAttributeUniqueness(Type typeToCheck)
		{
			var typeAttribute = (DependencyAttribute)Attribute.GetCustomAttribute(typeToCheck, typeof(DependencyAttribute));
			var properties = typeToCheck.GetProperties();
		}
	}
}
