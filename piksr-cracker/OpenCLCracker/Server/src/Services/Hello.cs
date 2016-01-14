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

namespace server.Services
{
    [Route("/api/hello")]
    [Route("/api/hello/{Name}")]
    public class Hello: IReturn<HelloResponse>
    {
        public string Name { get; set; }
    }

    public class HelloResponse
    {
        public string Result { get; set; }
    }

	[Authenticate]
    public class HelloService: Service
    {
        public object Any(Hello request)
        {
            return new HelloResponse {Result = "Hello, " + request.Name};
        }
    }
}

