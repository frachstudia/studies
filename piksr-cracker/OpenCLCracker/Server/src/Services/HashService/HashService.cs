using System;
using System.Linq;
using System.Net;
using Server.Logic.Patients;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.OrmLite;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceHost;
using servstack;
using ServiceStack;
using Common;
using System.Collections.Generic;

namespace server.Services
{
	public class HashService : Service
	{
		public IScheduler Scheduler { get; set; }

		public object Get (DtoHash req)
		{
			Tuple<Status, string> tuple = Scheduler.GetStatus (req.id);
			List<string> list = new List<string> ();
			list.Add (tuple.Item1.ToString ());
			list.Add(tuple.Item2);

			return new DtoHashStatusResponse (list);
		}

		public object Post (DtoHash req)
		{
			string result;

			//req.hash = "49d02d55ad10973b7b9d0dc9eba7fdf0";

			if (req.sum == "md5")
				result = Scheduler.AddJob (StringToByteArray (req.hash), Common.HashType.MD5, req.maxLength);
			else if (req.sum == "sha1")
				result = Scheduler.AddJob (StringToByteArray (req.hash), Common.HashType.SHA1, req.maxLength);
			else
				result = "error";
			//Console.WriteLine ("id = " + result);

			return new DtoHashIDResponse (result);
		}

		public static byte[] StringToByteArray (string hex)
		{
			return Enumerable.Range (0, hex.Length)
				.Where (x => x % 2 == 0)
				.Select (x => Convert.ToByte (hex.Substring (x, 2), 16))
				.ToArray ();
		}
	}
}

