using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection
{
    public class SimpleContainer : IContainer
    {
		private IList<RegisteredObject> _registeredObjects = new List<RegisteredObject>();

	    public void Register<TType, TConcrete>()
		    where TType : class
		    where TConcrete : class
	    {
		    _registeredObjects.Add(new RegisteredObject(typeof(TType), typeof(TConcrete)));
	    }

	    public T Resolve<T>()
	    {
		    return (T)ResolveObject(typeof(T));
	    }

	    private object ResolveObject(Type typeToResolve)
	    {
		    var registeredObject = _registeredObjects.FirstOrDefault(o => o.Type == typeToResolve);
		    if (registeredObject == null)
		    {
			    //
		    }
		    return GetInstance(registeredObject);
	    }

	    private object GetInstance(RegisteredObject registeredObject)
	    {
		    if (registeredObject.Instance == null ||
		        registeredObject.LifeCycle == LifeCycle.Transient)
		    {
			    var parameters = ResolveConstructorParameters(registeredObject);
			    registeredObject.CreateInstance(parameters.ToArray());
		    }
		    return registeredObject.Instance;
	    }
	}
}
