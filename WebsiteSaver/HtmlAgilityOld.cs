using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebsiteSaver.Enums;
using WebsiteSaver.Helpers;

namespace WebsiteSaver
{
	class HtmlAgilityOld
	{
		//key - web link, value - local link
		private Dictionary<string, string> _linkContainer = new Dictionary<string, string>();

		public DomainLimitEnum VisitOtherDomains { get; set; }
		public List<string> AllowedFormats { get; set; }

		public HtmlAgilityOld()
		{
			VisitOtherDomains = DomainLimitEnum.NoLimit;
		}

		public HtmlAgilityOld(DomainLimitEnum domaiLimitation)
		{
			VisitOtherDomains = domaiLimitation;
		}

		public HtmlAgilityOld(string allowedExtensions)
		{
			VisitOtherDomains = DomainLimitEnum.NoLimit;

			var splittedExtensions = allowedExtensions.Split(',');
			AllowedFormats = splittedExtensions.Select(x => x).ToList();
		}

		public HtmlAgilityOld(DomainLimitEnum domaiLimitation, string allowedExtensions)
		{
			VisitOtherDomains = domaiLimitation;

			var splittedExtensions = allowedExtensions.Split(',');
			AllowedFormats = splittedExtensions.Select(x => x).ToList();
		}

		public void DownloadWebsite(string uri, string folderPath, int depth = 0)
		{
			Log.Info($"Website downloading has started: {uri}");

			if (!Directory.Exists(folderPath))
			{
				throw new ArgumentException("Specified path wasn't found.");
			}

			var parsedUri = new Uri(uri);
			var websiteFolderPath = Path.Combine(folderPath, parsedUri.Host);

			Directory.CreateDirectory(websiteFolderPath);

			//GetPage(parsedUri, websiteFolderPath, depth);
		}

		//private async void GetPage(Uri uri, string folderToSave, int depth = 0)
		//{
		//	Log.Info($"Retrieving a web page: {uri.AbsoluteUri}");

		//	var httpClient = new HttpClient();
		//	var htmlPage = new HtmlDocument();

		//	//get page
		//	var result = await httpClient.GetStringAsync(uri);
		//	htmlPage.LoadHtml(result);

		//	//get title
		//	var pageTitle = GetTitle(htmlPage);

		//	//create directory
		//	var pageFilesPath = Path.Combine(folderToSave, $"{pageTitle}_files");
		//	Directory.CreateDirectory(pageFilesPath);

		//	// get files
		//	Log.Info($"Retrieving files related to {uri.AbsoluteUri}");
		//	DownloadFiles(htmlPage, pageFilesPath, uri);

		//	// save html file
		//	var localPageLink = Path.Combine(folderToSave, $"{pageTitle}.html");
		//	File.WriteAllText(localPageLink, result);

		//	_linkContainer.Add(uri.AbsoluteUri, localPageLink);

		//	// save other pages
		//	if (depth > 0)
		//	{
		//		var links = htmlPage.DocumentNode.SelectNodes("//a[@href]");

		//		foreach (var node in links)
		//		{
		//			var link = new Uri(node.Attributes["href"].Value);
		//			GetPage(link, folderToSave, depth--);
		//		}
		//	}
		//}

		//private string GetTitle(HtmlDocument htmlPage)
		//{
		//	return htmlPage.DocumentNode.SelectSingleNode("html/head/title").InnerText;
		//}

		//private void DownloadFiles(HtmlDocument htmlPage, string folderToSave, Uri uri)
		//{
		//	//var content = htmlPage.DocumentNode.Descendants().Where(x => x.Attributes["src"].Value != null).ToList();
		//	var content = htmlPage.DocumentNode.Descendants().ToList();
		//	var nodes = content.Where(x => x.Attributes.Contains(@"src")).ToList();

		//	var urls = htmlPage.DocumentNode.Descendants()
		//		.Select(e => e.GetAttributeValue("src", null))
		//		.Where(s => !String.IsNullOrEmpty(s)).ToList();

		//	if (!content.Any()) return;
		//	using (var webClient = new WebClient())
		//	{
		//		foreach (var node in content)
		//		{
		//			var srcValue = node.Attributes["src"].Value;

		//			Log.Info($"Saving file: {srcValue}");

		//			var fileUrl = new Uri(uri, srcValue);
		//			var filename = Path.GetFileName(fileUrl.AbsolutePath);

		//			var downloadedFilePath = Path.Combine(folderToSave, filename);
		//			webClient.DownloadFile(fileUrl, downloadedFilePath);

		//			node.Attributes["src"].Value = downloadedFilePath;
		//		}
		//	}
		//}
	}
}

