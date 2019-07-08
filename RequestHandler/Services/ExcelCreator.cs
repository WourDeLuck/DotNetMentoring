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

			var list = data.First().GetType().GetProperties();
			var headers = list.Select(x => x.Name).ToList();

			for (var i = 0; i < headers.Count; i++)
			{
				ws.Cell(1, i + 1).Value = headers[i];
			}

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