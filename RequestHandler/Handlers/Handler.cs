using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using RequestHandler.Models;

namespace RequestHandler.Handlers
{
	public class Handler : IHttpHandler
	{
		public void ProcessRequest(HttpContext context)
		{
			var query = context.Request.QueryString;
			if (query.ToString().Equals("hello")) 
			{
				context.Response.Write("<img src='https://vignette.wikia.nocookie.net/borderlands/images/6/6a/Steve_Heyo.png/revision/latest?cb=20130812064006'>");
			}

			var urlParams = context.Request.QueryString;
			ProcessRequest(urlParams);
		}

		public bool IsReusable => true;

		private CustomerOrdersModel ProcessRequest(NameValueCollection parameters)
		{
			int.TryParse(parameters["skip"], out var skipNumber);
			int.TryParse(parameters["take"], out var takeNumber);

			DateTime.TryParse(parameters["dateFrom"], out var dateFrom);
			DateTime.TryParse(parameters["dateTo"], out var dateTo);

			return new CustomerOrdersModel
			{
				Id = parameters["customer"],
				Skip = skipNumber,
				Take = takeNumber,
				DateFrom = dateFrom,
				DateTo = dateTo
			};
		}

		private void CheckDateParams(NameValueCollection parameters)
		{
			// validation for uniqueness of dateFrom and dateTo
		}
	}
}