using System.Configuration;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcMusicStore.Startup))]
namespace MvcMusicStore
{
    public partial class Startup
    {
	    public void Configuration(IAppBuilder app)
        {
			ConfigureAuth(app);

            ConfigureApp(app);

	        ConfigureLogging();
        }

	    public void ConfigureLogging()
	    {
		    bool.TryParse(ConfigurationManager.AppSettings["EnableLogging"], out var isLoggingEnabled);

			var hierarchy = (log4net.Repository.Hierarchy.Hierarchy)log4net.LogManager.GetRepository();
		    var rootLogger = hierarchy.Root;
		    rootLogger.Level = isLoggingEnabled ? hierarchy.LevelMap["ALL"] : hierarchy.LevelMap["OFF"];
		}
    }
}
