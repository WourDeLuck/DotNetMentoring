using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DependencyInjection.Attributes;

namespace DependencyInjection.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			DemonstrateContainer();
		}

		private static void DemonstrateContainer()
		{
			var container = new SimpleContainer();

			container.AddType(typeof(TestClass));
			var test = (TestClass) container.CreateInstance(typeof(TestClass));

			System.Console.WriteLine(test.GetType() == typeof(TestClass));
		}
	}

	[ImportConstructor]
	public class TestClass
	{
		private SomeClass _yo;

		public TestClass(SomeClass cl)
		{
			_yo = cl;
		}
	}

	public class SomeClass 
	{
		public SomeClass()
		{
			
		}
	}
}
