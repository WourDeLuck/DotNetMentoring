using System.IO;
using AngleSharp;
using AngleSharp.Dom;
using WebsiteCrawler.Helpers;
using WebsiteCrawler.Interfaces;

namespace WebsiteCrawler.Services
{
	public class FileService : IFileService
	{
		public void CreateDirectory(string folderPath)
		{
			Guard.ThrowIfStringIsNullOrEmpty(folderPath);

			Directory.CreateDirectory(folderPath);
		}

		public void SaveToHtmlFile(string fileLink, IDocument document)
		{
			Guard.ThrowIfStringIsNullOrEmpty(fileLink);

			using (TextWriter writer = File.CreateText(fileLink))
			{
				document.ToHtml(writer);
			}
		}

		public string[] GetFiles(string folderPath)
		{
			Guard.ThrowIfDirectoryNotExist(folderPath);

			return Directory.GetFiles(folderPath);
		}

		public string ReadText(string filePath)
		{
			Guard.ThrowIfFileNotExist(filePath);

			return File.ReadAllText(filePath);
		}
	}
}
