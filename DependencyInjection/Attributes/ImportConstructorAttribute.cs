using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DependencyInjection.Enums;

namespace DependencyInjection.Attributes
{
	// used to import constructor dependencies
	public class ImportConstructorAttribute : DependencyAttribute
	{
		public ImportConstructorAttribute()
		{
			DependencyType = ObjectType.ImportConstructor;
		}
	}
}
