namespace FileSystemService.Common.Helpers
{
	public static class StringFormatter
	{
		public static string EscapeDateTimeSymbols(this string dateTime)
		{
			return dateTime.Replace('/', '-').Replace(':', '-').Replace('.', '-');
		}
	}
}
