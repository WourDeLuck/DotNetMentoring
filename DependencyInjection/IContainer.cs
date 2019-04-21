using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection
{
	public interface IContainer
	{
		void Register<TType, TConcrete>()
			where TType : class
			where TConcrete : class;

		T Resolve<T>();
	}
}
