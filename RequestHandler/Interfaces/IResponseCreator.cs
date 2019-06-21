using System.Collections.Generic;
using System.Web;
using RequestHandler.Models;

namespace RequestHandler.Interfaces
{
	public interface IResponseCreator
	{
		void CreateResponse(IEnumerable<CustomerOrderViewModel> data, HttpResponse response);
	}
}
