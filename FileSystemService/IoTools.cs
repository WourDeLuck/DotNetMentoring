using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemService
{
	public class IoTools
	{
		public void MoveFile(string filePath, string destinationFolder)
		{
			CheckFileExistence(filePath);

			var fileName = Path.GetFileName(filePath);
			File.Move(filePath, Path.Combine(destinationFolder, fileName));

		}

		public void RenameFile(string filePath, string newFileName)
		{
			CheckFileExistence(filePath);

			var directory = Path.GetDirectoryName(filePath);
			File.Move(filePath, Path.Combine(directory, newFileName));
		}

		private void CheckFileExistence(string filePath)
		{
			if (!File.Exists(filePath))
			{
				throw new FileNotFoundException($"Specified file not found: {filePath}");
			}
		}
	}
}
