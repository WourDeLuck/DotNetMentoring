// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using SampleSupport;
using Task.Data;

// Version Mad01

namespace SampleQueries
{
	[Title("LINQ Module")]
	[Prefix("Linq")]
	public class LinqSamples : SampleHarness
	{

		private DataSource dataSource = new DataSource();

		[Category("Sup")]
		[Title("Where - Task 1")]
		[Description("This sample returns a list of clients whose orders sum is more than some value X")]
		public void Linq1()
		{
			var borders = new []{5000, 1000, 500, 200};

			foreach (var border in borders)
			{
				var filteredClients = dataSource.Customers
				.Where(x => x.Orders.Select(y => y.Total).Sum() < border)
				.Select(x => new
				{
					CustomerName = x.CustomerID,
					TotalPrice = x.Orders.Select(c => c.Total).Sum()
				});

				ObjectDumper.Write($"Show records under {border}");
				foreach (var client in filteredClients)
				{
					ObjectDumper.Write(client);
				}
			}
		}

		[Category("Sup")]
		[Title("Where - Task 2")]
		[Description("This sample returns a list of clients grouped with customers in the same region")]
		public void Linq2()
		{
			var customersAndSuppliersInSameCountry = dataSource.Customers.Join(dataSource.Suppliers, x => x.Country, y => y.Country,
				(customer, supplier) => new
				{
					customer.Country,
					Customer = customer.CustomerID,
					Supplier = supplier.SupplierName
				});

			var customersAndSuppliersInSameCity = dataSource.Customers.Join(dataSource.Suppliers, x => x.City, y => y.City,
				(customer, supplier) => new
				{
					customer.City,
					Customer = customer.CustomerID,
					Supplier = supplier.SupplierName
				});

			ObjectDumper.Write("Customers and Suppliers in the same country:");
			foreach (var sequence in customersAndSuppliersInSameCountry)
			{
				ObjectDumper.Write(sequence);
			}
			ObjectDumper.Write("Customers and Suppliers in the same city:");
			foreach (var sequence in customersAndSuppliersInSameCity)
			{
				ObjectDumper.Write(sequence);
			}
		}

		[Category("Sup")]
		[Title("Where - Task 3")]
		[Description("This sample returns a list of clients whose orders sum is more than X")]
		public void Linq3()
		{
			var totalBorder = 5000;

			var clients = dataSource.Customers
				.Where(x => x.Orders.Select(y => y.Total).Sum() > totalBorder)
				.Select(x => new
				{
					CustomerName = x.CustomerID,
					TotalPrice = x.Orders.Select(c => c.Total).Sum()
				});

			foreach (var client in clients)
			{
				ObjectDumper.Write(client);
			}
		}

		[Category("Sup")]
		[Title("Where - Task 4")]
		[Description("This sample returns a list of clients and the date of their first order")]
		public void Linq4()
		{
			var clients = dataSource.Customers.Select(x => new
			{
				CustomerName = x.CustomerID,
				FirstOrder = x.Orders.OrderBy(y => y.OrderDate).Select(y => y.OrderDate).FirstOrDefault().ToString("MM/yyyy")
			});

			foreach (var client in clients)
			{
				ObjectDumper.Write(client);
			}
		}

		[Category("Sup")]
		[Title("Where - Task 6")]
		[Description("This sample returns all clients with non-numeric postal code, empty region, or international phone code")]
		public void Linq6()
		{
			var clients = dataSource.Customers
				.Where(x => (x.PostalCode != null && x.PostalCode.All(char.IsNumber)) || 
				(string.IsNullOrEmpty(x.Region)) ||
				(!x.Phone.Contains('(') && !x.Phone.Contains(')')))
				.Select(x => new
				{
					x.CustomerID,
					x.Region,
					x.PostalCode,
					x.Phone
				});

			foreach (var client in clients)
			{
				ObjectDumper.Write(client);
			}
		}
	}
}
