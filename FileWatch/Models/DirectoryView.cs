using System.IO;

namespace FileWatch.Models
{
	public class DirectoryView
	{
		/// <summary>
		/// Gets or sets a full path to the unit.
		/// </summary>
		public string FullPath { get; set; }
		
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
		public DirectoryView(string path)
		{
			var fileInfo = new DirectoryInfo(path);

			Name = fileInfo.Name;
			ParentFolder = fileInfo.Parent?.Name;
			FullPath = fileInfo.FullName;
		}
	}
}