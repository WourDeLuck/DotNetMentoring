using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection.Models
{
	public class RegisteredObject
	{
		public Type Type { get; set; }

		public Type Concrete { get; set; }

		public string LifeCycle { get; set; }

		public RegisteredObject(Type type, Type concrete)
		{
			Type = type;
			Concrete = concrete;
		}

		public RegisteredObject(Type type, Type concrete, string lifeCycle)
		{
			Type = type;
			Concrete = concrete;
			LifeCycle = lifeCycle;
		}

		public override bool Equals(object obj)
		{
			return obj is RegisteredObject @object &&
			       EqualityComparer<Type>.Default.Equals(Type, @object.Type) &&
			       EqualityComparer<Type>.Default.Equals(Concrete, @object.Concrete);
		}

		public override int GetHashCode()
		{
			var hashCode = 307679827;
			hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(Type);
			hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(Concrete);
			return hashCode;
		}
	}
}
