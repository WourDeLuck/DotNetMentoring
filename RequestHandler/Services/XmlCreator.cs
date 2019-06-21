using System.Collections.Generic;
using System.Linq;
using System.Web;
using RequestHandler.Interfaces;
using RequestHandler.Models;
using System.Xml.Linq;

namespace RequestHandler.Services
{
	public class XmlCreator : IResponseCreator
	{
		public void CreateResponse(IEnumerable<CustomerOrderViewModel> data, HttpResponse response)
		{
			var doc = new XDocument(
				new XDeclaration("1.0", "UTF-16", null),
				new XElement("Orders", data.Select(o => new XElement("Order", 
				new XElement("OrderID", o.OrderId), 
				new XElement("Customer", o.CustomerName), 
				new XElement("Date", o.OrderDate),
				new XElement("Products", o.ProductsNames.Select(x => new XElement("Product", x))),
				new XElement("Address", o.ShipAddress),
				new XElement("Total", o.TotalPrice)))));

			response.ContentType = "text/xml";
			response.Clear();
			response.Write(doc.ToString());
			response.End();
		}
	}
}