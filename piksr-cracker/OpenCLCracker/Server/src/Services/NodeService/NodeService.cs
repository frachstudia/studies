using System;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using Server.Logic.Patients;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.OrmLite;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceHost;
using Server.Logic;
using servstack;

namespace server.Services.NodeService
{
	[Authenticate]
	public class NodeService : Service
	{
		//public Scheduler scheduler;
		//public Scheduler scheduler = new Scheduler();

		public IScheduler Scheduler { get; set; }

		public NodeService() {
			//Db.CreateTableIfNotExists<Node>();
			//scheduler.AddNode ("localhost", 1111);
			//scheduler.AddNode ("localhost", 2222);
		}

		[RequiredRole("Admin")]
		public object Get (DtoNode req)
		{
			return new DtoNodeResponse (Scheduler.aGetNodes());
		}

		[RequiredRole("Admin")]
		public object Post (DtoNode req)
		{
			Scheduler.AddNode (req.address, req.port);
			return new HttpResult {StatusCode = HttpStatusCode.Created};/*
			if (Scheduler.aGetNodes ().Contains (req)) {
				Console.WriteLine ("Taki już jest");
				return new HttpResult { StatusCode = HttpStatusCode.Conflict };
			}
			else {
				Scheduler.AddNode (req.address, req.port);
				return new HttpResult {StatusCode = HttpStatusCode.Created};
			}*/
		}

		[RequiredRole("Admin")]
		public object Delete (DtoNode req) 
		{
			string[] temp = req.id.Split ('_');
			Scheduler.RemoveNode (temp[0], int.Parse(temp[1]));
			return new HttpResult { StatusCode = HttpStatusCode.OK };/*
			if (Scheduler.aGetNodes ().Find(p => p.id == req.id) != null) {
				Scheduler.RemoveNode (req.address, req.port);
				return new HttpResult { StatusCode = HttpStatusCode.OK };
			}
			else {
				Console.WriteLine ("Nie ma takiego");
				return new HttpResult {StatusCode = HttpStatusCode.Conflict};
			}*/
		}
	}
}