using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using WebsiteCrawler.Enums;
using WebsiteCrawler.Extensions;
using WebsiteCrawler.Helpers;
using WebsiteCrawler.Interfaces;

namespace WebsiteCrawler.Services
{
    public class WebsiteProcessor : IWebsiteProcessor
    {
		//key - web link, value - local link
		private Dictionary<string, string> _linkContainer;
		private readonly HttpClient _client;
	    private readonly IFileService _fileService;
	    private string _domain;

		public DomainLimitEnum VisitOtherDomains { get; set; }
		public List<string> AllowedFormats { get; set; }

	    public WebsiteProcessor()
	    {
		    _linkContainer = new Dictionary<string, string>();
			_client = new HttpClient();
			_fileService = new FileService();
			AllowedFormats = new List<string>();
		    VisitOtherDomains = DomainLimitEnum.NoLimit;
	    }

		public async void DownloadWebsite(string uri, string localFolderPath, int depth = 0)
		{
			Log.Info($"Website downloading has started: {uri}");

			Guard.ThrowIfDirectoryNotExist(localFolderPath);
			Guard.ThrowIfStringIsNullOrEmpty(uri);
			Guard.ThrowIfUriIsInvalid(uri);

			var parsedUri = new Uri(uri);
			_domain = parsedUri.Host;
			var websiteFolderPath = Path.Combine(localFolderPath, _domain.ToSafeFileName());

			_fileService.CreateDirectory(websiteFolderPath);

			if (VisitOtherDomains == DomainLimitEnum.InsideOriginalUrlOnly) depth = 0;

			await GetPageAsync(uri, websiteFolderPath, depth);

			UpdateLinksInFiles(websiteFolderPath);
		}

		private async Task GetPageAsync(string uri, string folderToSave, int depth = 0)
		{
			Log.Info($"Retrieving a web page: {uri}");

			if (!Guard.IsValidUri(uri)) return;

			if (VisitOtherDomains == DomainLimitEnum.InsideCurrentDomainOnly)
			{
				var validateUri = new Uri(uri).Host;
				if (!validateUri.Equals(_domain)) return;
			}

			var result = await _client.GetAsync(uri);
			var responseContent = await result.Content.ReadAsStringAsync();

			var config = Configuration.Default.WithDefaultLoader();
			var context = BrowsingContext.New(config);

			var document = await context.OpenAsync(req => req.Content(responseContent));

			var pageTitle = document.Title;

			// create files directory
			var pageFilesPath = Path.Combine(folderToSave, $"{pageTitle}_files".ToSafeFileName());
			Log.Info($"Creating a content folder of the web page: {pageFilesPath}");
			_fileService.CreateDirectory(pageFilesPath);

			Log.Info($"Retrieving files related to {uri}");
			DownloadFiles(document, pageFilesPath, uri);

			var mainPageLink = Path.Combine(folderToSave, $"{pageTitle}.html".ToSafeFileName());

			Log.Info($"Saving the web page {mainPageLink}");
			_fileService.SaveToHtmlFile(mainPageLink, document);

			UpdateLinkContainer(uri, mainPageLink);

			if (depth <= 0) return;

			var links = document
				.QuerySelectorAll("a")
				.Select(x => x.Attributes["href"].Value)
				.ToList();

			depth--;

			foreach (var node in links)
			{
				await GetPageAsync(node, folderToSave, depth);
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

					var downloadedFilePath = Path.Combine(pageFilesFolderPath, filename.ToSafeFileName());

					if (!AllowedFormats.Contains(Path.GetExtension(downloadedFilePath))) continue;
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

	    private async void UpdateLinksInFiles(string localWebSiteFolder)
	    {
			Log.Info("Updating links in folder:");
		    Guard.ThrowIfDirectoryNotExist(localWebSiteFolder);

		    var htmlFilesCollection = _fileService.GetFiles(localWebSiteFolder).Where(x => Path.GetExtension(x).Equals(".html")).ToList();

			foreach (var file in htmlFilesCollection)
			{
				Log.Info($"Processing {file}");
			    var fileContent = _fileService.ReadText(file);

				var config = Configuration.Default.WithDefaultLoader();
			    var context = BrowsingContext.New(config);

			    var document = await context.OpenAsync(req => req.Content(fileContent));

			    var nodes = document
				    .QuerySelectorAll("a").Where(x => x.HasAttribute("href")).ToList();

			    foreach (var node in nodes)
			    {
				    var href = node.GetAttribute("href");

				    if (_linkContainer.ContainsKey(href))
				    {
					    node.SetAttribute("href", _linkContainer[href]);
				    }
			    }

				_fileService.SaveToHtmlFile(file, document);
			}
	    }
	}
}
