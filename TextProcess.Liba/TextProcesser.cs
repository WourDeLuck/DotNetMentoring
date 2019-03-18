using System;
using System.Collections.Generic;
using TextProcess.DataAccess;

namespace TextProcess.Liba
{
	/// <summary>
	/// Class for text processing.
	/// </summary>
    public class TextProcesser
    {
		/// <summary>
		/// Reads text and returns first character of each string.
		/// </summary>
		/// <param name="path">Path to the file.</param>
		/// <returns>Collection of chars.</returns>
	    public List<char> ReadTextAndReturnChars(string path)
	    {
		    var strings = GetStringArrayFromText(path);

		    var chars = new List<char>();

		    for (var i=0; i<strings.Length; i++)
		    {
			    try
			    {
				    chars.Add(GetFirstLetterOfString(strings[i]));
			    }
			    catch (ArgumentException e)
			    {
				    Console.WriteLine($"String with index {i} has thrown an exception: {e.Message}");
			    }
		    }

		    return chars;
	    }

		/// <summary>
		/// Gets first character of the string.
		/// </summary>
		/// <param name="str">Original string.</param>
		/// <returns>First character of a string.</returns>
	    public char GetFirstLetterOfString(string str)
	    {
		    var firstPosition = 0;

		    if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
		    {
			    throw new ArgumentNullException(str, "Value cannot be null or empty string.");
		    }

		    return str.ToCharArray()[firstPosition];
	    }

		/// <summary>
		/// Reads text and separates it into string collection.
		/// </summary>
		/// <param name="path">Path to the file.</param>
		/// <returns>Collection of strings.</returns>
	    private string[] GetStringArrayFromText(string path)
	    {
		    IoReader ioReader = new IoReader();

		    var text = ioReader.ReadFile(path);

		    string[] separatingChars = { "\r\n" };
		    return text.Split(separatingChars, StringSplitOptions.None);
	    }
	}
}