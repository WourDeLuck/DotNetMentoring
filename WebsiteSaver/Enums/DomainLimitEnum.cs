using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteSaver.Enums
{
	public enum DomainLimitEnum
	{
		NoLimit = 1,
		InsideCurrentDomainOnly = 2,
		InsideOriginalUrlOnly = 3
	}
}