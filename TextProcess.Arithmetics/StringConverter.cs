using System;
using System.Linq;

namespace TextProcess.Arithmetics
{
    public class StringConverter
    {
	    public int ConvertToInt(string text)
	    {
		    return (int) PerformCalculations(text);
	    }

	    public long ConvertToLong(string text)
	    {
		    return (long)PerformCalculations(text);
	    }

	    private double PerformCalculations(string text)
	    {
			var charArr = text.ToCharArray();

		    var decadeForMultiply = 10;

		    return charArr
			    .Select((t, i) => (t - '0') * Math.Pow(decadeForMultiply, charArr.Length - 1 - i))
			    .Sum();
		}
	}
}
