using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileSystemService.Common;

namespace FileSystemService
{
	public class IoTools
	{
		public string MoveFile(string filePath, string destinationFolder)
		{
			Guard.ThrowFileExistence(filePath);

			var fileName = Path.GetFileName(filePath);
			var newDestinationPath = Path.Combine(destinationFolder, fileName);

			File.Move(filePath, newDestinationPath);
			Log.Info($"File has been moved to the destination folder: {destinationFolder}");

			return newDestinationPath;
		}

		public string RenameFile(string filePath, string newFileName)
		{
			Guard.ThrowFileExistence(filePath);

			var directory = Path.GetDirectoryName(filePath);
			var renamedFilePath = Path.Combine(directory, newFileName);
			File.Move(filePath, renamedFilePath);

			Log.Info($"File has been renamed: {filePath}");

			return renamedFilePath;
		}
	}
}
