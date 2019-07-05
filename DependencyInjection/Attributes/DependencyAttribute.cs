using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DependencyInjection.Enums;

namespace DependencyInjection.Attributes
{
	public class DependencyAttribute : Attribute
	{
		public ObjectType DependencyType { get; set; }

		public Type Type { get; set; } = null;
	} 
}
