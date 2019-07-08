using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace RequestHandler.Models
{
	public class CustomerOrderViewModel
	{
		[DisplayName("Order ID")]
		public int OrderId { get; set; }

		[DisplayName("Customer")]
		public string CustomerName { get; set; }

		[DisplayName("Order Date")]
		public DateTime? OrderDate { get; set; }

		[DisplayName("Products")]
		public IList<string> ProductsNames { get; set; }

		[DisplayName("Address")]
		public string ShipAddress { get; set; }

		[DisplayName("Total")]
		public decimal TotalPrice { get; set; }
	}
}