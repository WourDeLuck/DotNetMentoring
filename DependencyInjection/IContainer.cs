using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection
{
	public interface IContainer
	{
		//void Register<TType, TConcrete>()
		//	where TType : class
		//	where TConcrete : class;

		//T Resolve<T>();

		void AddType(Type concrete);

		void AddType(Type concrete, Type type);

		void AddAssemnly(Assembly executingAssembly);

		object CreateInstance(Type concrete);

		object CreateInstance<TConcrete>();
	}
}
