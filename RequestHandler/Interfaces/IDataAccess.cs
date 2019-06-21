using System.Collections.Generic;
using System.Web;
using RequestHandler.Models;

namespace RequestHandler.Interfaces
{
	public interface IDataAccess
	{
		IList<CustomerOrderViewModel> GetData(DataRequestModel data);
	}
}