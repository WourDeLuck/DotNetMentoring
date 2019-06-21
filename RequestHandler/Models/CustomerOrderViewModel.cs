using System;
using System.Collections.Generic;

namespace RequestHandler.Models
{
	public class CustomerOrderViewModel
	{
		public int OrderId { get; set; }

		public string CustomerName { get; set; }

		public DateTime? OrderDate { get; set; }

		public IList<string> ProductsNames { get; set; }

		public string ShipAddress { get; set; }

		public decimal TotalPrice { get; set; }
	}
}