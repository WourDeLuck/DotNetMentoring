using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ClosedXML.Excel;
using RequestHandler.Interfaces;
using RequestHandler.Models;

namespace RequestHandler.Services
{
	public class ExcelCreator : IResponseCreator
	{
		public void CreateResponse(IEnumerable<CustomerOrderViewModel> data, HttpResponse response)
		{
			var workbook = new XLWorkbook();
			var ws = workbook.Worksheets.Add("Report");

			ws.Cell(1, 1).Value = "OrderID";
			ws.Cell(1, 2).Value = "Customer Name";
			ws.Cell(1, 3).Value = "Order Date";
			ws.Cell(1, 4).Value = "Products";
			ws.Cell(1, 5).Value = "Address";
			ws.Cell(1, 6).Value = "Total";

			var query = data.Select(x => new
			{
				x.OrderId,
				x.CustomerName,
				x.OrderDate,
				Products = string.Join(", ", x.ProductsNames),
				x.ShipAddress,
				x.TotalPrice
			});

			ws.Cell(2, 1).Value = query.AsEnumerable();

			response.Clear();
			response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
			response.AddHeader("content-disposition", "attachment;filename=\"Journal.xlsx\"");

			using (var memoryStream = new MemoryStream())
			{
				workbook.SaveAs(memoryStream);
				memoryStream.WriteTo(response.OutputStream);
				memoryStream.Close();
			}

			response.End();
		}
	}
}