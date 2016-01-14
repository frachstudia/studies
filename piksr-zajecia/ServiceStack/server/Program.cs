using System;

namespace server
{
	internal class Program
	{
		private static void Main ()
		{
			var appHost = new AppHost ();
			appHost.Init ();
			ushort port = 8888;
			var listeningOn = string.Format ("http://*:{0}/", port);
			appHost.Start (listeningOn);

			Console.WriteLine ("Listening on {0}...", port);
			Console.WriteLine ("Press ENTER to exit.");

			Console.ReadLine ();
		}
	}
}

