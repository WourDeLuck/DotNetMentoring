using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DependencyInjection.Enums;

namespace DependencyInjection.Models
{
	public class RegisteredObject
	{
		public DependencyEnum ObjectType { get; set; }

		public Type ConcreteType { get; set; } 

		public Type Type { get; set; }

		public object Instance { get; set; }

		public RegisteredObject(Type concrete, Type type)
		{
			ConcreteType = concrete;
			Type = type;
		}

		public RegisteredObject(Type concrete)
		{
			ConcreteType = concrete;
		}

		public override bool Equals(object obj)
		{
			return obj is RegisteredObject @object &&
				   EqualityComparer<Type>.Default.Equals(ConcreteType, @object.ConcreteType) &&
				   EqualityComparer<Type>.Default.Equals(Type, @object.Type);
		}

		public override int GetHashCode()
		{
			var hashCode = -1155721637;
			hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(ConcreteType);
			hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(Type);
			return hashCode;
		}
	}
}
