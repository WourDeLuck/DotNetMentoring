using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using RequestHandler.Helpers;
using RequestHandler.Interfaces;
using RequestHandler.Models;
using RequestHandler.Services;

namespace RequestHandler.Handlers
{
	public class Handler : IHttpHandler
	{
		public bool IsReusable => true;

		public void ProcessRequest(HttpContext context)
		{
			try
			{
				var requestParams = GetParametersByContentType(context.Request);

				// easter egg
				if (requestParams.ToString().Equals("hello"))
				{
					context.Response.Clear();
					context.Response.Write("<img src='https://vignette.wikia.nocookie.net/borderlands/images/6/6a/Steve_Heyo.png/revision/latest?cb=20130812064006'>");
					context.Response.End();
				}

				var processedRequestData = ProcessRequestParameters(requestParams);

				var dataAccess = new DataAccess();
				var filteredData = dataAccess.GetData(processedRequestData);

				GetResponse(context, filteredData);
			}
			catch (Exception e)
			{
				context.Response.Write(e.Message);
			}
		}

		private NameValueCollection GetParametersByContentType(HttpRequest request)
		{
			return request.ContentType.Equals("application/x-www-form-urlencoded") ? request.Form : request.QueryString;
		}

		private DataRequestModel ProcessRequestParameters(NameValueCollection parameters)
		{
			int.TryParse(parameters["skip"], out var skipNumber);
			var takeSuccess = int.TryParse(parameters["take"], out var takeNumber);

			var customerId = parameters["customer"];

			Guard.ValidateString(customerId);

			var parsedData = new DataRequestModel
			{
				Id = customerId,
				Skip = skipNumber,
				Take = takeSuccess ? takeNumber : (int?)null
			};

			SelectDateParameter(parameters, parsedData);

			return parsedData;
		}

		private void SelectDateParameter(NameValueCollection parameters, DataRequestModel parsedData)
		{
			var dfSuccess = DateTime.TryParse(parameters["dateFrom"], out var dateFrom);
			var dtSuccess = DateTime.TryParse(parameters["dateTo"], out var dateTo);

			if (dfSuccess && dtSuccess)
			{
				throw new ArgumentException("Only one of parameters dateFrom/dateTo is allowed.");
			}
			else if (dfSuccess)
			{
				parsedData.DateFrom = dateFrom;
			}
			else if (dtSuccess)
			{
				parsedData.DateTo = dateTo;
			}
		}

		private void GetResponse(HttpContext context, IEnumerable<CustomerOrderViewModel> data)
		{
			var accept = context.Request.ContentType;

			var creator = accept.Equals("text/xml") || accept.Equals("application/xml")
				? (IResponseCreator) new XmlCreator()
				: (IResponseCreator) new ExcelCreator();

			creator.CreateResponse(data, context.Response);
		}
	}
}