using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RequestHandler.Tests
{
	[TestClass]
	public class XmlResponseTests
	{
		private HttpClient _client;

		[TestInitialize]
		public void Initialize()
		{
			_client = new HttpClient();
		}

		[TestMethod]
		public async Task TestMethod1()
		{
			var uri = "http://localhost:51003/Report?hello";
			var result = await _client.GetAsync(uri);

			var res = result.Content.ReadAsStringAsync();


			//var config = new HttpConfiguration();
			////configure web api
			//config.MapHttpAttributeRoutes();

			//using (var server = new HttpServer(config))
			//{

			//	var client = new HttpClient(server);

			//	string url = "http://localhost/Report?hello";

			//	var request = new HttpRequestMessage
			//	{
			//		RequestUri = new Uri(url),
			//		Method = HttpMethod.Get
			//	};

			//	//request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			//	using (var response = await client.SendAsync(request))
			//	{
			//		var res = response.Content.ReadAsStringAsync();
			//	}
			//}
		}

		[TestMethod]
		public async Task Xml_GetAllCustomerOrders()
		{
			var uri = "http://localhost:51003/Report?customer=BERGS";
			_client.DefaultRequestHeaders.Add("content-type", "text/xml");
			var result = await _client.GetAsync(uri);

			var res = result.Content.ReadAsStringAsync();
		}
	}
}
