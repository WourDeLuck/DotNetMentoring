using System;
using System.Linq;

namespace Arithmetics
{
    public class StringConverter
    {
	    public int ConvertToInt(string text)
	    {
		    var charArr = text.ToCharArray();

		    var decadeForMultiply = 10;

		    return (int) charArr
				.Select((t, i) => (t - '0') * Math.Pow(decadeForMultiply, charArr.Length - 1 - i))
				.Sum();
	    }
    }
}
