using System.IO;
using FileSystemService.Common.Helpers;
using logging = FileSystemService.Common.Resources.Logging;

namespace FileSystemService
{
	public class IoTools
	{
		public string MoveFile(string filePath, string destinationFolder)
		{
			Guard.ThrowIfFileNotExist(filePath);

			var fileName = Path.GetFileName(filePath);
			var newDestinationPath = Path.Combine(destinationFolder, fileName);

			File.Move(filePath, newDestinationPath);
			Log.Info($"{logging.FileMoved}: {newDestinationPath}");

			return newDestinationPath;
		}

		public string RenameFile(string filePath, string newFileName)
		{
			Guard.ThrowIfFileNotExist(filePath);

			var directory = Path.GetDirectoryName(filePath);
			var renamedFilePath = Path.Combine(directory, newFileName);
			File.Move(filePath, renamedFilePath);

			Log.Info($"{logging.FileRenamed}: {renamedFilePath}");

			return renamedFilePath;
		}

		public int GetFileCount(string filepath)
		{
			var filesCount = 0;
			var directoryName = Path.GetDirectoryName(filepath);

			if (directoryName != null)
			{
				filesCount = Directory.GetFiles(directoryName).Length;
			}

			return filesCount;
		}
	}
}