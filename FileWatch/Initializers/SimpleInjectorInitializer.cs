using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileWatch.Interfaces;
using FileWatch.Services;
using SimpleInjector;

namespace FileWatch.Initializers
{
	public static class SimpleInjectorInitializer
	{
		static Container _container;

		public static void Initialize()
		{
			_container = new Container();

			// 2. Configure the container (register)
			_container.Register<IFileSystemWrapper, FileSystemWrapper>();

			// 3. Verify your configuration
			_container.Verify();
		}
	}
}