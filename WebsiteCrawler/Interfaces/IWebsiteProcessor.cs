namespace WebsiteCrawler.Interfaces
{
	public interface IWebsiteProcessor
	{
		void DownloadWebsite(string uri, string localFolderPath, int depth = 0);
	}
}
