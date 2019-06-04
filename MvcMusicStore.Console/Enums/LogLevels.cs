using System.ComponentModel.DataAnnotations;

namespace MvcMusicStore.Console.Enums
{
	public enum LogLevels
	{
		[Display(Name="DEBUG")]
		Debug = 0,

		[Display(Name="INFO")]
		Info = 1,

		[Display(Name="WARN")]
		Warn = 2,

		[Display(Name="ERROR")]
		Error = 3,

		[Display(Name="FATAL")]
		Fatal = 4
	}
}
