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
		// I am getting confused a lot, but type is Type and concrete type is its implementation.
		private IList<RegisteredObject> _registeredObjects = new List<RegisteredObject>();

		// register
		public void AddType(Type concrete)
		{
			// TODO: attribute validation

			var registeredObject = new RegisteredObject(concrete);

			if (_registeredObjects.Contains(registeredObject)) return;

			_registeredObjects.Add(registeredObject);
		}

		// register
	    public void AddType(Type concrete, Type type)
	    {
			// TODO: attribute validation

		    var registeredObject = new RegisteredObject(concrete, type);

			if (_registeredObjects.Contains(registeredObject)) return;

			_registeredObjects.Add(registeredObject);
		}

		// register
	    public void AddAssembly(Assembly executingAssembly)
	    {
			var types = GetAssemblyTypes(executingAssembly);

		    foreach (var type in types)
		    {
			    AddType(type);
		    }
	    }

		// resolve
	    public object CreateInstance(Type concrete)
	    {
		    return ResolveType(concrete);
	    }

		// resolve
	    public TConcrete CreateInstance<TConcrete>()
	    {
		    return (TConcrete) ResolveType(typeof(TConcrete));
	    }

	    private object ResolveType(Type type)
	    {
		    object instance = null;

			var registeredObject = _registeredObjects.First(x => x.ConcreteType == type);
		    if (registeredObject.Instance != null)
		    {
			    return registeredObject.Instance;
		    }

			var attribute = (DependencyAttribute)Attribute.GetCustomAttribute(type, typeof(DependencyAttribute));
		    if (attribute.DependencyType == ObjectType.ImportConstructor)
		    {
			    instance = ResolveConstructor(type);
		    }
		    else if (attribute.DependencyType == ObjectType.ImportProperty)
		    {
			    instance = ResolveProperties(type);
		    }

		    registeredObject.Instance = instance;

		    return instance;
		}

		private static IEnumerable<Type> GetAssemblyTypes(Assembly assembly) => assembly.GetTypes().Where(type => Attribute.IsDefined(type, typeof(DependencyAttribute)));

	    private object ResolveConstructor(Type concrete)
	    {
			var constructor = concrete.GetConstructors().FirstOrDefault();

		    if (constructor == null) throw new ArgumentException("There is no public constructors for this type.");

		    var parameterTypes = constructor.GetParameters().Select(p => p.ParameterType).ToList();

		    if (!parameterTypes.Any())
		    {
			    return Activator.CreateInstance(concrete);
		    }

		    var dependencies = parameterTypes.Select(ResolveConstructor).ToArray();

		    return Activator.CreateInstance(concrete, dependencies);
	    }

	    private object ResolveProperties(Type concrete)
	    {
		    var instance = Activator.CreateInstance(concrete);

			// get properties
			var properties = concrete.GetProperties().Where(x => Attribute.IsDefined(x, typeof(DependencyAttribute)));
			
		    foreach (var prop in properties)
		    {
			    var registeredObject = _registeredObjects.First(x => x.ConcreteType == prop.PropertyType);
			    var ins = registeredObject.Instance ?? ResolveConstructor(prop.PropertyType);
			    registeredObject.Instance = ins;
				prop.SetValue(instance, registeredObject.Instance);
		    }

		    return instance;
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
