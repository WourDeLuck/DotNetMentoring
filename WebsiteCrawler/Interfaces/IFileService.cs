using AngleSharp.Dom;

namespace WebsiteCrawler.Interfaces
{
	public interface IFileService
	{
		void CreateDirectory(string folderPath);

		void SaveToHtmlFile(string fileLink, IDocument document);

		string[] GetFiles(string folderPath);

		string ReadText(string filePath);
	}
}
