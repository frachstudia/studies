using System.Collections.Generic;
using System.Runtime.Serialization;

namespace server.Services.NodeService
{
	public class DtoNodeResponse
	{
		public DtoNodeResponse(List<DtoNode> list)
		{
			Nodes = list;
		}

		public List<DtoNode> Nodes { get; set; }
	}
}