using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DependencyInjection.Enums;

namespace DependencyInjection.Attributes
{
	// used to import property dependencies
	public class ImportPropertyAttribute : DependencyAttribute
	{
		public ImportPropertyAttribute()
		{
			DependencyType = ObjectType.ImportProperty;
		}
	}
}
