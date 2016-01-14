using System;
using Server.Logic;
using ServiceStack.ServiceHost;

namespace server.Services.NodeService
{
	[Route ("/api/nodes", "GET")]
	[Route ("/api/nodes/{id}", "POST DELETE")]
	public class DtoNode : IReturn<DtoNodeResponse>
	{
		public string id { get; set; }

		public string address {get; set; }

		public int port { get; set; }
	}
}