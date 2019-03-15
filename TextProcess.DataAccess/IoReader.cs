using System.IO;
using TextProcess.Common;

namespace TextProcess.DataAccess
{
    public class IoReader
    {
	    public string ReadFile(string path)
	    {
			Guard.CheckPathExistence(path);

		    return File.ReadAllText(path);
	    }
	}
}
