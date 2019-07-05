using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DependencyInjection.Enums;

namespace DependencyInjection.Attributes
{
	// used to export dependencies
	public class ExportAttribute : DependencyAttribute
	{
		public ExportAttribute()
		{
			DependencyType = ObjectType.Export;
		}

		public ExportAttribute(Type type)
		{
			Type = type;
			DependencyType = ObjectType.ExportWithType;
		}
	}
}
