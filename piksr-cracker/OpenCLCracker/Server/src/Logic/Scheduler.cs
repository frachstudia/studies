using System;
using Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NetMQ;
using NetMQ.Sockets;
using NLog;
using System.Text;

namespace servstack
{
	public interface IScheduler
	{
		String AddJob (byte[] hash, HashType type, int maxLength);

		Tuple<Status, String> GetStatus (String id);

		void AddNode (String hostName, int port);

		void RemoveNode (String hostName, int port);

		List<Tuple<String, int>> GetNodes ();

		List<server.Services.NodeService.DtoNode> aGetNodes ();
	}

	public class Scheduler : IScheduler
	{
		private NetMQContext Context;

		private Dictionary<Tuple<String,int>, RequestSocket> Sockets =
			new Dictionary<Tuple<String,int>, RequestSocket> ();

		private PullSocket ReceiverSocket;

		private static Logger logger = LogManager.GetCurrentClassLogger ();

		public Scheduler ()
		{
			Context = NetMQContext.Create ();
			ReceiverSocket = Context.CreatePullSocket ();
			ReceiverSocket.Bind ("tcp://0.0.0.0:10000");

			new Thread (this.catchAck).Start ();

			logger.Info ("Scheduler started");
		}

		class LocalJob
		{
			public Dictionary<RequestSocket, int> Nodes = new Dictionary<RequestSocket,int> ();
			public int NoResponds = 0;

			public String Password = null;
			public Common.Status Status = Common.Status.NONE;
		}

		private Dictionary<String, LocalJob> LocalJobs = new Dictionary<String, LocalJob> ();

		private Dictionary<int, String> RemoteIdToLocal = new Dictionary<int, string> ();

		private void catchAck ()
		{
			while (true) {
				var msg = ReceiverSocket.ReceiveMessage ();
				logger.Debug ("Received message");

				if (msg [0].ConvertToString () == "ack") {

					lock (LocalJobs) {

						var id = RemoteIdToLocal [msg [1].ConvertToInt32 ()];
						LocalJob localJob = LocalJobs [id];

						localJob.NoResponds++;

						if (msg [2].ConvertToString () == "SOLVED") {

							localJob.Status = Common.Status.SOLVED;
							localJob.Password = msg [3].ConvertToString ();
							logger.Info ("Found password for {0}, {1}", id, localJob.Password);

							if (localJob.NoResponds < localJob.Nodes.Count) {
								foreach (var node in localJob.Nodes) {
									var msg2 = new NetMQMessage ();
									msg2.Append ("abort");
									msg2.Append (node.Value);
									node.Key.SendMessage (msg2);
									node.Key.ReceiveMessage ();
								}
							}
						} else if (localJob.NoResponds == localJob.Nodes.Count) {
							localJob.Status = Common.Status.PASSWORD_NOT_FOUND;
							logger.Info ("Failed to solve password for job {0}", id);
						}
					}
				} else {
					logger.Error ("Invalid message: {0}", msg [0].ToString ());
				}
			}
		}

		private const byte ASCII_0 = 0x30;
		private const byte ASCII_z = 0x7a;

		public String AddJob (byte[] hash, HashType type, int maxLength)
		{
			var localId = Guid.NewGuid ().ToString ();
			var localJob = new LocalJob ();

			var starters = Enumerable.Range (ASCII_0, ASCII_z - ASCII_0 + 1).ToList ();

			lock (LocalJobs) {
			
				LocalJobs.Add (localId, localJob);

				int i = 0;
				foreach (var rs in Sockets.Values) {
				
					var request = new NetMQMessage ();
					request.Append ("job");
					request.Append (hash);
					request.Append (type.ToString ());
					request.Append (maxLength);

					for (int a = 0; a < starters.Count; a++) {
						if (a % Sockets.Count == i) {
							request.Append (Encoding.ASCII.GetString (new byte[] { (byte)starters [a] }));
						}
					}

					rs.SendMessage (request);

					var recv = rs.ReceiveMessage ();
					int id = recv [0].ConvertToInt32 ();
					RemoteIdToLocal.Add (id, localId);

					localJob.Nodes.Add (rs, id);

					i++;
				}

				localJob.Status = Common.Status.PROCESSING;

			}

			return localId;
		}

		public Tuple<Status, String> GetStatus (String id)
		{
			var job = LocalJobs [id];
			return Tuple.Create (job.Status, job.Password);
		}

		public void AddNode (String hostName, int port)
		{
			var rs = Context.CreateRequestSocket ();
			rs.Connect (String.Format ("tcp://{0}:{1}", hostName, port));

			Sockets.Add (Tuple.Create (hostName, port), rs);

			logger.Info ("Added node {0}:{1}", hostName, port);

			ReceiverSocket.Connect (String.Format ("tcp://{0}:{1}", hostName, port + 1));
		}


		public void RemoveNode (String hostName, int port)
		{
			var node = Tuple.Create (hostName, port);
			Sockets.Remove (node);
			logger.Info ("Removed node {0}:{1}", hostName, port);
		}

		public List<Tuple<String, int>> GetNodes ()
		{
			var list = new List<Tuple<String, int>> ();

			foreach (var s in Sockets) {
				list.Add (s.Key);
			}

			return list;
		}

		public List<server.Services.NodeService.DtoNode> aGetNodes ()
		{
			var list = new List<server.Services.NodeService.DtoNode> ();

			foreach (var s in Sockets) {
				list.Add (new server.Services.NodeService.DtoNode{address = s.Key.Item1, port = s.Key.Item2, id = s.Key.Item1 + ":" + s.Key.Item2});
			}

			return list;
		}
	}
}

