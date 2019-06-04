using System.Configuration;
using Microsoft.Owin;
using Owin;
using System.Diagnostics;

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

			ConfigurePerfomanceCounter();
        }

	    public void ConfigureLogging()
	    {
		    bool.TryParse(ConfigurationManager.AppSettings["EnableLogging"], out var isLoggingEnabled);

			var hierarchy = (log4net.Repository.Hierarchy.Hierarchy)log4net.LogManager.GetRepository();
		    var rootLogger = hierarchy.Root;
		    rootLogger.Level = isLoggingEnabled ? hierarchy.LevelMap["ALL"] : hierarchy.LevelMap["OFF"];
		}

	    public void ConfigurePerfomanceCounter()
	    {
		    var pcCategoryName = "CustomCounters";

		    if (!PerformanceCounterCategory.Exists(pcCategoryName))
		    {
			    var counters = new CounterCreationDataCollection();

			    var successfullLogins = new CounterCreationData
			    {
				    CounterName = "# of successfull logins",
				    CounterHelp = "Total number of successfull logins",
				    CounterType = PerformanceCounterType.NumberOfItems32
			    };
			    counters.Add(successfullLogins);

			    var successfullLogoffs = new CounterCreationData
			    {
					CounterName = "# of successfull logoffs",
				    CounterHelp = "Total number of successfull logoffs",
				    CounterType = PerformanceCounterType.NumberOfItems32
				};
			    counters.Add(successfullLogoffs);

			    var averageTimePerOperation = new CounterCreationData
			    {
				    CounterName = "average time per operation",
				    CounterHelp = "Average duration per operation execution",
				    CounterType = PerformanceCounterType.AverageTimer32
			    };
			    counters.Add(averageTimePerOperation);

			    var averageDurationBase = new CounterCreationData
			    {
				    CounterName = "average time per operation base",
				    CounterHelp = "Average duration per operation execution base",
				    CounterType = PerformanceCounterType.AverageBase
			    };
			    counters.Add(averageDurationBase);

				PerformanceCounterCategory.Create(pcCategoryName, "Custom Performance Counter Category", PerformanceCounterCategoryType.SingleInstance, counters);
		    }
	    }
    }
}
