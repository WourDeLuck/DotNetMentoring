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
using Task.Extensions;

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
				filteredClients.WriteToApp();
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
			customersAndSuppliersInSameCountry.WriteToApp();

			ObjectDumper.Write("Customers and Suppliers in the same city:");
			customersAndSuppliersInSameCity.WriteToApp();
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

			clients.WriteToApp();
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
		[Title("Where - Task 5")]
		[Description("This sample returns a list of clients and the date of their first order sorted by year and then by month")]
		public void Linq5()
		{
			var clients = dataSource.Customers.Select(x => new
			{
				CustomerName = x.CustomerID,
				FirstOrder = x.Orders.OrderBy(y => y.OrderDate).Select(y => y.OrderDate).FirstOrDefault(),
				Orders = x.Orders.OrderByDescending(g => g.Total)
			}).OrderBy(x => x.FirstOrder.Year).ThenBy(x => x.FirstOrder.Month).ThenBy(x => x.CustomerName);

			foreach (var client in clients)
			{
				ObjectDumper.Write(client);
				foreach (var order in client.Orders)
				{
					ObjectDumper.Write(order);
				}
			}
		}

		[Category("Sup")]
		[Title("Where - Task 6")]
		[Description("This sample returns all clients with non-numeric postal code, empty region, or international phone code")]
		public void Linq6()
		{
			var clients = dataSource.Customers
				.Where(x => x.PostalCode != null && x.PostalCode.All(char.IsNumber) || string.IsNullOrEmpty(x.Region) || !x.Phone.Contains('(') && !x.Phone.Contains(')'))
				.Select(x => new
				{
					x.CustomerID,
					x.Region,
					x.PostalCode,
					x.Phone
				});

			clients.WriteToApp();
		}

		[Category("Sup")]
		[Title("Where - Task 7")]
		[Description("This sample returns grouped products by category, then by their availability, order last group by price")]
		public void Linq7()
		{
			var products = dataSource.Products
				.GroupBy(x => x.Category)
				.Select(x => new
				{
					Category = x.Key,
					Products = x.GroupBy(y => y.UnitsInStock > 0)
					.Select(u => new
					{
						IsAvailable = u.Key,
						MatchingProducts = u.ToList()
					}).ToList()
				}).ToList();

			var categorySelector = products.LastOrDefault().Category;

			var lastGroup = products.LastOrDefault().Products.Select(x => x.MatchingProducts.OrderBy(u => u.UnitPrice)).Select(x => new
			{
				IsAvailable = x.All(y => y.UnitsInStock > 0),
				MatchingProducts = x.ToList()
			}).ToList();

			products.RemoveAll(x => x.Category == categorySelector);
			products.Add(new
			{
				Category = categorySelector,
				Products = lastGroup
			});

			foreach (var client in products)
			{
				ObjectDumper.Write(client.Category);
				foreach (var av in client.Products)
				{
					ObjectDumper.Write(av.IsAvailable);
					foreach (var seq in av.MatchingProducts)
					{
						ObjectDumper.Write(seq);
					}
				}
			}
		}

		[Category("Sup")]
		[Title("Where - Task 8")]
		[Description("This sample returns grouped by the price products")]
		public void Linq8()
		{
			var priceRange = new[] { 100, 50, 0 };

			var grouped = dataSource.Products
				.GroupBy(i => priceRange.First(c => c <= i.UnitPrice))
				.Select(x => new
				{
					PriceRange = x.Key,
					Products = x.Select(t => new
					{
						Name = t.ProductName,
						Price = t.UnitPrice
					}).OrderBy(j => j.Price)
				});

			foreach (var group in grouped)
			{
				switch (group.PriceRange)
				{
					case 0:
						ObjectDumper.Write("Cheap (0-50):");
						break;
					case 50:
						ObjectDumper.Write("Normal price (50-100):");
						break;
					case 100:
						ObjectDumper.Write("Expensive (>100):");
						break;
				}

				group.Products.WriteToApp();
			}
		}

		[Category("Sup")]
		[Title("Where - Task 9")]
		[Description("This sample returns average income of each city and average amount of clients per city")]
		public void Linq9()
		{
			var averagePricePerCity = dataSource.Customers
				.GroupBy(x => x.City)
				.Select(x => new
				{
					City = x.Key,
					AverageIncome = x.Where(y => y.Orders.Any()).Select(y => y.Orders.Select(u => u.Total).Sum()).Average(),
					AverageAmountOfOrders = x.Select(y => y.Orders.Length).Sum() / x.Count()
				});

			averagePricePerCity.WriteToApp();
		}

		[Category("Sup")]
		[Title("Where - Task 10")]
		[Description("This sample returns average income of each city and average amount of clients per city")]
		public void Linq10()
		{
			
		}
	}
}
