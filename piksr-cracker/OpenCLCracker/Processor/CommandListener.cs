using System;
using System.Threading;
using NetMQ;
using System.Text;
using Common;
using NLog;
using System.Collections.Generic;
using NetMQ.Sockets;
using Processor.PasswordMatcher;

namespace Processor
{
	public class CommandListener
	{
		private static Logger logger = LogManager.GetCurrentClassLogger ();

		private int Port { get; set; }

		private Dictionary<int, Job> Jobs { get; set; }

		private NetMQContext Context;
		private ResponseSocket RequestSock;
		private PushSocket ResponseSock;

		public Queuer Queuer { get; set; }

		public CommandListener (int port, Dictionary<int, Job> jobs)
		{
			this.Port = port;
			this.Jobs = jobs;
			this.Context = NetMQContext.Create ();
			this.RequestSock = Context.CreateResponseSocket ();
			this.RequestSock.Bind ("tcp://0.0.0.0:" + Port);
			logger.Info ("Bound to TCP port {0}", Port);

			this.ResponseSock = Context.CreatePushSocket ();
			this.ResponseSock.Bind ("tcp://0.0.0.0:" + (Port + 1));
			logger.Info ("Bound callback to TCP port {0}", (Port + 1));
		}

		private bool shutdown = false;

		public void run ()
		{
			var hashTypes = new Dictionary<String, HashType> ();
			hashTypes.Add ("MD5", HashType.MD5);
			hashTypes.Add ("SHA1", HashType.SHA1);

			while (!shutdown) {
				var request = RequestSock.ReceiveMessage ();

				if (request [0].ConvertToString () == "job") {
					var job = new Job () {
						Id = GetNewId (),
						Status = Status.QUEUED,

						Hash = request [1].ToByteArray (),
						Type = hashTypes [request [2].ConvertToString ()],
						MaxLength = request [3].ConvertToInt32 (),
					};

					var keySpace = new List<String> ();
					for (int i = 4; i < request.FrameCount; i++) {
						keySpace.Add (request [i].ConvertToString ());
					}
								
					job.KeySpace = keySpace.ToArray ();

					logger.Info ("Queue new job {0}: {1} hash: {2}", job.Id, job.Type,
						BitConverter.ToString (job.Hash).Replace ("-", "").ToLower ());

					lock (Jobs) {
						Jobs.Add (job.Id, job);
						Monitor.PulseAll (Jobs);
					}

					var returned = new NetMQMessage ();
					returned.Append (job.Id);
					RequestSock.SendMessage (returned);
				} else if (request [0].ConvertToString () == "abort") {
					int id = request [1].ConvertToInt32 ();

					lock (Jobs) {

						if (Jobs [id].Status == Status.PROCESSING) {
							logger.Info ("Received request to abort job {0}", id);
							Jobs [id].Status = Status.ABORTED;
							Queuer.Abort ();
						} else {
							logger.Info ("Received request to abort job {0}, ignoring.", id);
						}
					}

					var msg2 = new NetMQMessage ();
					msg2.Append ("ok");
					RequestSock.SendMessage (msg2);
				} else {
					logger.Error ("Invalid message: {0}", request [0].ToString ());
					var msg2 = new NetMQMessage ();
					msg2.Append ("ok");
					RequestSock.SendMessage (msg2);
				}
			}
		}

		private static int GetNewId ()
		{
			return BitConverter.ToInt32 (Guid.NewGuid ().ToByteArray (), 0);
		}

		public void Shutdown ()
		{
			shutdown = true;
		}

		public void SendAck (Job job)
		{
			var msg = new NetMQMessage ();
			msg.Append ("ack");
			msg.Append (job.Id);
			msg.Append (job.Status.ToString ());
			msg.Append (job.Password == null ? "" : job.Password);

			ResponseSock.SendMessage (msg);
		}
	}
}

