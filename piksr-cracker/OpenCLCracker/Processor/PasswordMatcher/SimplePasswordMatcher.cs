using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Security.Cryptography;
using NLog;
using Common;
using System.Threading;

namespace Processor.PasswordMatcher
{
	public class SimplePasswordMatcher : IPasswordMatcher
	{
		private static Logger logger = LogManager.GetCurrentClassLogger ();

		private const byte ASCII_0 = 0x30;
		private const byte ASCII_z = 0x7a;

		private volatile bool ShouldAbort = false;

		public SimplePasswordMatcher ()
		{
			logger.Debug ("Starting SimplePasswordMatcher");
		}

		public String SearchPassword (byte[] hash, HashType type, int maxLength, String[] keySpace)
		{
			logger.Info ("Starting looking for password with {0} hash: {1}.", 
				type, BitConverter.ToString (hash).Replace ("-", "").ToLower ());

			int iterations = 0;

			HashAlgorithm hashAlg;

			switch (type) {
			case HashType. MD5:
				hashAlg = MD5.Create ();
				break;
			case HashType.SHA1:
				hashAlg = SHA1.Create ();
				break;
			default:
				throw new NotImplementedException ("hash not supported");
			}

			foreach (var keyStart in keySpace) {
				logger.Info ("Starting key space: \"{0}\"", keyStart);

				ShouldAbort = false;

				for (int length = 1; length <= maxLength - keyStart.Length; length++) {
					logger.Info ("Trying key length {0}", keyStart.Length + length);

					byte[] p = Enumerable.Repeat (ASCII_0, length).ToArray ();

					while (true) {

						var password = keyStart + Encoding.ASCII.GetString (p);

						if (ShouldAbort) {
							logger.Warn ("Aborting as requested.");
							return null;
						}

						if (Enumerable.SequenceEqual 
							(hash, hashAlg.ComputeHash (Encoding.ASCII.GetBytes (password)))) {
							logger.Info ("Match found after {0} iterations: {1}.", iterations, password);
							return password;
						}

						p [p.Length - 1]++;
						iterations++;

						for (int i = p.Length - 1; i > 0; i--) {
							if (p [i] > ASCII_z) {
								p [i - 1]++;
								p [i] = ASCII_0;
							}
						}

						if (p [0] > ASCII_z) {
							break;
						}
					}
				}
			}

			logger.Debug ("No password found during {0} iterations.", iterations);
			return null;
		}

		public void Abort ()
		{
			ShouldAbort = true;
		}
	}
}

