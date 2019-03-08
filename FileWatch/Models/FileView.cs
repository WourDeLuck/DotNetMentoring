using System.IO;

namespace FileWatch.Models
{
	/// <summary>
	/// Model that represents a unit in a file system.
	/// </summary>
	public class FileView
	{
		/// <summary>
		/// Gets or sets a full path to the unit.
		/// </summary>
		public string FullPath { get; set; }

		/// <summary>
		/// Gets or sets an extensin of the unit, if it is not a folder.
		/// </summary>
		public string Extension { get; set; }

		/// <summary>
		/// Gets or sets a name of unit.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets parent folder of a unit.
		/// </summary>
		public string ParentFolder { get; set; }

		/// <summary>
		/// Public constructor that parses string path into class.
		/// </summary>
		/// <param name="path"></param>
		public FileView(string path)
		{
			var fileInfo = new FileInfo(path);

			Name = fileInfo.Name;
			ParentFolder = fileInfo.DirectoryName;
			FullPath = fileInfo.FullName;
			Extension = fileInfo.Extension;
		}
	}
}