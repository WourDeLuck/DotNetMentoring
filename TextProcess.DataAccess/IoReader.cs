using System.IO;
using TextProcess.Common;

namespace TextProcess.DataAccess
{
	/// <summary>
	/// Class for file system access.
	/// </summary>
    public class IoReader
    {
		/// <summary>
		/// Reads text from a file of a specified path.
		/// </summary>
		/// <param name="path">Path to the file.</param>
		/// <returns>Text of the file.</returns>
	    public string ReadFile(string path)
	    {
			Guard.ThrowPathExistence(path);

		    return File.ReadAllText(path);
	    }
	}
}
