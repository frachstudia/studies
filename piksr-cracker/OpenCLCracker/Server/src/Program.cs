using System;
using System.Linq;

namespace Server
{
	internal class Program
	{

		public static byte[] StringToByteArray (string hex)
		{
			return Enumerable.Range (0, hex.Length)
				.Where (x => x % 2 == 0)
				 .Select (x => Convert.ToByte (hex.Substring (x, 2), 16))
				 .ToArray ();
		}


		private static void Main ()
		{/*
			var scheduler = new servstack.Scheduler ();
			scheduler.AddNode ("localhost", 15000);
			scheduler.AddNode ("localhost", 16000);
			scheduler.AddNode ("localhost", 17000);


			scheduler.AddJob (
				StringToByteArray ("900150983cd24fb0d6963f7d28e17f73"), Common.HashType.MD5, 4);
*/

			var appHost = new AppHost ();
			appHost.Init ();
			ushort port = 8888;
			string listeningOn = string.Format ("http://*:{0}/", port);
			appHost.Start (listeningOn);

			Console.WriteLine ("AppHost created at {0}, listening on {1}", DateTime.Now, listeningOn);
			Console.WriteLine ("Press ENTER to exit...");
			Console.ReadLine ();
		}
	}
}

