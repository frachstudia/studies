using System;
using Server.Logic;
using ServiceStack.ServiceHost;

namespace server.Services
{
	[Route ("/api/hash", "POST")]
	[Route ("/api/hash/{id}", "GET")]
	public class DtoHash : IReturn<DtoHashIDResponse>
	{
		public string id { get; set; }

		public string hash { get; set; }

		public string sum { get; set; }

		public int maxLength { get; set; }
	}
}