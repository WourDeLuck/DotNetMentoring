﻿using System;

namespace RequestHandler.Models
{
	public class DataRequestModel
	{
		public string Id { get; set; }

		public DateTime? DateFrom { get; set; }

		public DateTime? DateTo { get; set; }

		public int? Take { get; set; }

		public int Skip { get; set; }
	}
}