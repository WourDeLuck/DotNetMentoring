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
			_container.Register<ISystemFactory, FileSystemFactory>();
			_container.Register<ISystemFactory, DirectorySystemFactory>();

			// 3. Verify your configuration
			_container.Verify();
		}
	}
}