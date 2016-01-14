using System;
using System.Text;
using Common;
using NLog;
using System.Collections.Generic;
using Processor.PasswordMatcher;
using System.Threading;

namespace Processor
{
	public class Queuer
	{
		private static Logger logger = LogManager.GetCurrentClassLogger ();

		private Dictionary<int, Job> Jobs { get; set; }

		private IPasswordMatcher PasswordMatcher { get; set; }

		private volatile bool shutdown = false;

		private CommandListener Listener;

		public Queuer (Dictionary<int, Job> jobs, IPasswordMatcher passwordMatcher, CommandListener listener)
		{
			this.Jobs = jobs;
			this.PasswordMatcher = passwordMatcher;
			this.Listener = listener;
		}

		private volatile bool Aborted = false;

		public void Abort ()
		{
			Aborted = true;
			PasswordMatcher.Abort ();
		}

		public void run ()
		{
			logger.Info ("Started queuer");

			while (!shutdown) {
				Job foundJob = null;
				Aborted = false;

				lock (Jobs) {

					while (true) {
						foreach (Job j in Jobs.Values) {
							if (j.Status == Status.QUEUED) {
								foundJob = j;
							}
						}

						if (foundJob != null) {
							break;
						}

						Monitor.Wait (Jobs);
					}

					foundJob.Status = Status.PROCESSING;
				}

				logger.Info ("Dequeued job with ID {0}", foundJob.Id);

				String password = PasswordMatcher.SearchPassword (
					                  foundJob.Hash, foundJob.Type, foundJob.MaxLength, foundJob.KeySpace);
				if (!Aborted) {
					lock (Jobs) {
						if (password != null) {
							foundJob.Status = Status.SOLVED;
							foundJob.Password = password;
						} else {
							foundJob.Status = Status.PASSWORD_NOT_FOUND;
							foundJob.Password = null;
						}

						Listener.SendAck (foundJob);
					}
				}
			}
		}

		public void Shutdown ()
		{
			shutdown = true;
		}
	}
}

