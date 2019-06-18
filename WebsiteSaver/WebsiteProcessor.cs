using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using WebsiteSaver.Enums;
using WebsiteSaver.Helpers;

namespace WebsiteSaver
{
	public class WebsiteProcessor
	{
		//key - web link, value - local link
		private Dictionary<string, string> _linkContainer = new Dictionary<string, string>();
		private HttpClient _client = new HttpClient();

		//public DomainLimitEnum VisitOtherDomains { get; set; }
		//public List<string> AllowedFormats { get; set; }

		public void DownloadWebsite(string uri, string localFolderPath, int depth = 0)
		{
			Log.Info($"Website downloading has started: {uri}");

			if (!Directory.Exists(localFolderPath))
			{
				throw new ArgumentException("Specified path wasn't found.");
			}

			var parsedUri = new Uri(uri);
			var websiteFolderPath = Path.Combine(localFolderPath, parsedUri.Host);

			Directory.CreateDirectory(websiteFolderPath);

			GetPage(uri, websiteFolderPath);
		}

		public async void GetPage(string uri, string folderToSave, int depth = 0)
		{
			Log.Info($"Retrieving a web page: {uri}");

			//change to HTTPClient
			//var config = Configuration.Default.WithDefaultLoader();
			//var context = BrowsingContext.New(config);
			//var document = await context.OpenAsync(uri);

			var result = await _client.GetAsync(uri);
			var responseContent = await result.Content.ReadAsStringAsync();

			var config = Configuration.Default.WithDefaultLoader();
			var context = BrowsingContext.New(config);

			var document = await context.OpenAsync(req => req.Content(responseContent));

			var pageTitle = document.Title;

			// create files directory
			var pageFilesPath = Path.Combine(folderToSave, $"{pageTitle}_files");
			Directory.CreateDirectory(pageFilesPath);

			Log.Info($"Retrieving files related to {uri}");
			DownloadFiles(document, pageFilesPath, uri);

			var mainPageLink = Path.Combine(folderToSave, $"{pageTitle}.html");

			using (TextWriter writer = File.CreateText(mainPageLink))
			{
				Log.Info($"Saving web page to {mainPageLink}");
				document.ToHtml(writer);
			}

			if (depth <= 0) return;

			var links = document
				.QuerySelectorAll("a")
				.Select(x => x.Attributes["href"].Value)
				.ToList();

			foreach (var node in links)
			{
				GetPage(node, folderToSave, depth--);
			}
		}

		private void DownloadFiles(IDocument htmlPage, string pageFilesFolderPath, string uri)
		{
			var selectors = htmlPage.All.Where(x => x.HasAttribute("src")).ToList();

			using (var webClient = new WebClient())
			{
				foreach (var node in selectors)
				{
					var srcValue = node.Attributes["src"].Value;

					Log.Info($"Saving file: {srcValue}");

					var fileUrl = new Uri(new Uri(uri), srcValue);
					var filename = Path.GetFileName(fileUrl.AbsolutePath);

					var downloadedFilePath = Path.Combine(pageFilesFolderPath, filename);
					webClient.DownloadFile(fileUrl, downloadedFilePath);

					node.SetAttribute("src", downloadedFilePath);
					UpdateLinkContainer(fileUrl.AbsoluteUri, downloadedFilePath);
				}
			}
		}

		private void UpdateLinkContainer(string webLink, string localLink)
		{
			if (!_linkContainer.ContainsKey(webLink) && !_linkContainer.ContainsValue(localLink))
			{
				_linkContainer.Add(webLink, localLink);
			}
		}

		//private void UpdateRelatedLinks(IDocument htmlPage)
		//{
		//	var selector = "a";

		//	var items = htmlPage.QuerySelectorAll(selector);
		//	//var links = items.Select(x => (IHtmlAnchorElement) x.Href )
		//}
	}
}