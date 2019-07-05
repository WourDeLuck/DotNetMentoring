using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DependencyInjection.Attributes;
using DependencyInjection.Enums;
using DependencyInjection.Models;

namespace DependencyInjection
{
    public class SimpleContainer : IContainer
    {
		private IList<RegisteredObject> _registeredObjects = new List<RegisteredObject>();

		public void AddType(Type concrete)
		{
			// attribute validation

			var registeredObject = new RegisteredObject(concrete);

			if (_registeredObjects.Contains(registeredObject)) return;

			_registeredObjects.Add(registeredObject);
		}

	    public void AddType(Type concrete, Type type)
	    {
			// attribute validation

		    var registeredObject = new RegisteredObject(concrete, type);

			if (_registeredObjects.Contains(registeredObject)) return;

			_registeredObjects.Add(registeredObject);
		}

	    public void AddAssemnly(Assembly executingAssembly)
	    {
			var types = GetAssemblyTypes(executingAssembly);
	    }


	    public object CreateInstance(Type concrete)
	    {
		    var registeredObject = _registeredObjects.First(x => x.ConcreteType == concrete);
		    object instance = null;

		    var attribute = (DependencyAttribute) Attribute.GetCustomAttribute(concrete, typeof(DependencyAttribute));
		    if (attribute.DependencyType == ObjectType.ImportConstructor)
		    {
			    instance = ResolveConstructor(concrete);
		    }
		    else
		    {
			    var properties = concrete.GetProperties().Where(x => Attribute.IsDefined(x, typeof(DependencyAttribute)));
		    }

		    registeredObject.Instance = instance;

		    return instance;
	    }

	    public object CreateInstance<TConcrete>()
	    {
		    throw new NotImplementedException();
	    }

		private static IEnumerable<Type> GetAssemblyTypes(Assembly assembly) => assembly.GetTypes().Where(type => Attribute.IsDefined(type, typeof(DependencyAttribute)));

	    private object ResolveConstructor(Type concrete)
	    {
			//  object instance = null;
			//  var constructor = concrete.GetConstructors().FirstOrDefault();
			//  if (constructor != null)
			//  {
			//   var parameters = constructor.GetParameters();
			//instance = Activator.CreateInstance()
			//  }

		    object instance = null;

			var constructor = concrete.GetConstructors().FirstOrDefault();
			var constructorProperties = constructor.GetParameters();

			IList<ParameterInfo> parametersToResolve = new List<ParameterInfo>();
			foreach (var p in constructorProperties)
			{
				var regObj = new RegisteredObject(p.ParameterType);
				if (_registeredObjects.Contains(regObj))
				{
					parametersToResolve.Add(p);
				}
			}

			//var constructor = concrete.GetConstructors().FirstOrDefault();

			//if (constructor != null)
			//{
			// var parameters = constructor.GetParameters()
			//  .Select(parameter => ResolveConstructor(parameter.ParameterType))
			//  .ToArray();

			// instance = Activator.CreateInstance(concrete, parameters);
			//}
			return instance;
		}

	    private object ResolveProperties(Type concrete)
	    {
		    object instance = null;

		    var properties = concrete.GetProperties().Where(x => Attribute.IsDefined(x, typeof(DependencyAttribute)));


		    return instance;
	    }

	    private object ResolveParameterInstance(Type concrete)
	    {
		    var registeredType = _registeredObjects.First(x => x.ConcreteType == concrete);

		    if (registeredType != null && registeredType.Instance != null)
		    {
			    return registeredType.Instance;
		    }
		    else
		    {
				var constructor = concrete.GetConstructors().FirstOrDefault();

				if (constructor != null)
				{
					var parameters = constructor.GetParameters()
						.Select(parameter => ResolveParameterInstance(parameter.ParameterType))
						.ToArray();

					var instance = Activator.CreateInstance(concrete, parameters);
					registeredType.Instance = instance;
					return instance;
				}
				else
				{
					throw new ArgumentException();
				}
			}
	    }

	    //private ConstructorInfo SelectConstructor(Type type)
	    //{
		   // var constructors = type
			  //  .GetConstructors()
			  //  .Where(c => c.GetParameters().All((o) => _registeredObjects.Contains(o.ParameterType)))
			  //  .OrderByDescending(c => c.GetParameters().Count());

		   // var constructor = constructors.First();

		   // if (constructor == null)
		   // {
			  //  throw new ArgumentException("No public constructors were found for this type.");
		   // }

		   // return constructor;
	    //}
	}
}
