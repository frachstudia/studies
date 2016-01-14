using System;
using Processor.PasswordMatcher;
using System.Threading;
using Common;
using System.Collections.Generic;
using System.Linq;
namespace Processor
{
	class MainClass
	{
		private const int DEFAULT_PORT = 15000;

		private static Dictionary<int, Job> Jobs = new Dictionary<int, Job> ();

		public static byte[] StringToByteArray (string hex)
		{
			return Enumerable.Range (0, hex.Length)
				.Where (x => x % 2 == 0)
				.Select (x => Convert.ToByte (hex.Substring (x, 2), 16))
				.ToArray ();
		}

		public static void Main (string[] args)
		{
			int port = DEFAULT_PORT;

			if (args.Length > 0) {
				port = int.Parse (args [0]);
			}

			var passwordMatcher = new OpenCLPasswordMatcher ();

			var listener = new CommandListener (port, Jobs);
			var queuer = new Queuer (Jobs, passwordMatcher, listener);

			listener.Queuer = queuer;

			var listenerThread = new Thread (listener.run);
			var queuerThread = new Thread (queuer.run);

			queuerThread.Start ();
			listenerThread.Start ();

			listenerThread.Join ();
			queuerThread.Join ();

		}
	}
}
