using System;

namespace TextProcess.Liba
{
    public class TextProcesser
    {
	    public char GetFirstLetterOfString(string path)
	    {
		    var firstPosition = 0;

		    if (string.IsNullOrEmpty(path))
		    {
			    throw new ArgumentNullException(path, "Value cannot be null or empty string.");
		    }

		    if (string.IsNullOrWhiteSpace(path))
		    {
			    throw new ArgumentException("String cannot be empty or whitespace.", path);
		    }

		    return path.ToCharArray()[firstPosition];
	    }
	}
}
