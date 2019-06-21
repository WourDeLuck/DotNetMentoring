using System;
using System.Collections.Generic;
using System.Linq;
using RequestHandler.Interfaces;
using RequestHandler.Models;

namespace RequestHandler.Services
{
	public class DataAccess : IDataAccess
	{
		public IList<CustomerOrderViewModel> GetData(DataRequestModel data)
		{
			using (var db = new NorthwindEntities())
			{
				var customer = db.Customers.FirstOrDefault(c => c.CustomerID.Equals(data.Id));

				if (customer == null)
				{
					throw new ArgumentException($"Customer with Id {data.Id} wasn't found");
				}

				var filterExpression = CreateFilter(data);

				var listOfOrders = customer.Orders
					.Where(filterExpression)
					.OrderBy(o => o.OrderID)
					.Skip(data.Skip);

				if (data.Take.HasValue)
				{
					listOfOrders = listOfOrders.Take(data.Take.Value);
				}

				return PutDataToView(listOfOrders).ToList();
			}
		}

		private IEnumerable<CustomerOrderViewModel> PutDataToView(IEnumerable<Order> data)
		{
			return data.Select(x => new CustomerOrderViewModel
			{
				OrderId = x.OrderID,
				CustomerName = x.Customer.ContactName,
				OrderDate = x.OrderDate,
				ProductsNames = x.Order_Details.Select(d => d.Product.ProductName).ToList(),
				ShipAddress = x.ShipAddress,
				TotalPrice = x.Order_Details.Select(d => d.UnitPrice * d.Quantity).Sum()
			});
		}

		private Func<Order, bool> CreateFilter(DataRequestModel data)
		{
			Func<Order, bool> filter = order => true;

			if (data.DateFrom != null)
			{
				filter = order => order.OrderDate > data.DateFrom;
			}
			else if (data.DateTo != null)
			{
				filter = order => order.OrderDate < data.DateTo;
			}

			return filter;
		}
	}
}