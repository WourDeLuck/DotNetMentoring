using System;

namespace TextProcess
{
	public class TextProcesser
	{
		public char GetFirstLetterOfString(string text)
		{
			var firstPosition = 0;

			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentNullException(text, "Value cannot be null or empty string.");
			}

			if (string.IsNullOrWhiteSpace(text))
			{
				throw new ArgumentException("String cannot be empty or whitespace.", text);
			}

			return text.ToCharArray()[firstPosition];
		}
	}
}
