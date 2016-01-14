using System;
using Common;
using System.Collections.Generic;

namespace server.Services
{
	public class DtoHashIDResponse
	{
		//public Status status { get; set; }
		public string id { get; set; }

		public DtoHashIDResponse (string pass)
		{
			id = pass;
		}
	}

	public class DtoHashStatusResponse
	{
		public List<string> result { get; set; }

		public DtoHashStatusResponse (List<string> pass)
		{
			result = pass;
		}
	}
}