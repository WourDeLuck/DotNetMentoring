using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RequestHandler.Models;

namespace RequestHandler.Services
{
	public class JournalService
	{
		public void GetData(CustomerOrdersModel data)
		{
			using (var db = new NorthwindEntities())
			{
				var query = db.Orders
					.Where(o => o.CustomerID.Equals(data.Id) && (o.OrderDate > data.DateFrom && o.OrderDate < data.DateTo))
					.Skip(data.Skip).Take(data.Take)
					.OrderBy(o => o.OrderID)
					.ToList();
			}
		}
	}
}