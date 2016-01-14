using System;
using Cloo.Bindings;
using Cloo;
using System.IO;
using System.Security.Cryptography;
using Common;
using NLog;
using System.Text;
using System.Collections.Generic;

namespace Processor.PasswordMatcher
{
	public class OpenCLPasswordMatcher : IPasswordMatcher
	{
		private const String MD5_OPENCL_FILE = @"md5.cl";


		private static Logger logger = LogManager.GetCurrentClassLogger ();

		private ComputeContext Context;
		private ComputeDevice Device;
		private ComputeKernel Kernel;

		public OpenCLPasswordMatcher ()
		{
			if (ComputePlatform.Platforms.Count == 0) {
				Console.WriteLine ("Cound not find any OpenCL platforms");
				Environment.Exit (1);
			}

			var platform = ComputePlatform.Platforms [0];

			logger.Info ("Found {0} computing devices:", platform.Devices.Count);

			foreach (var d in platform.Devices) {
				logger.Info ("* {0}", d.Name);
			}

			Context = new ComputeContext (ComputeDeviceTypes.All,	
				new ComputeContextPropertyList (platform), null, IntPtr.Zero);

			Device = Context.Devices [0];

			logger.Info ("Using first device.");

			// load opencl source
			StreamReader streamReader = new StreamReader (MD5_OPENCL_FILE);
			string clSource = streamReader.ReadToEnd ();
			streamReader.Close ();

			// create program with opencl source
			ComputeProgram program = new ComputeProgram (Context, clSource);

			// compile opencl source
			try {
				program.Build (null, null, null, IntPtr.Zero);
			} catch (Exception e) {
				logger.Error ("Build log: " + program.GetBuildLog(Device));
				throw e;
			}

			// load chosen kernel from program
			Kernel = program.CreateKernel ("crackMD5");
		}

		public void Abort ()
		{

		}

		public String SearchPassword (byte[] hash, HashType type, int maxLength, String[] keySpace)
		{
			if (type != HashType.MD5) {
				throw new NotImplementedException ("sums other than MD5 not supported");
			}

			if (maxLength > 6) {
				throw new NotImplementedException ("doesn't support longer passwords than 7");
			}

			var joinedKeySpace = new List<byte> ();

			foreach (var k in keySpace) {
				if (k.Length > 1) {
					throw new NotImplementedException ("doesn't support longer keyspaces than 1");
				}

				joinedKeySpace.AddRange (Encoding.ASCII.GetBytes (k));
			}
				
			byte[] resultData = new byte[20];
			byte[] keyspaceJoined = joinedKeySpace.ToArray ();

			var resultBuffer = new ComputeBuffer<byte> (Context, ComputeMemoryFlags.WriteOnly | ComputeMemoryFlags.CopyHostPointer, resultData);
			var hashBuffer = new ComputeBuffer<byte> (Context, ComputeMemoryFlags.ReadOnly | ComputeMemoryFlags.CopyHostPointer, hash);
			var keyspaceBuffer = new ComputeBuffer<byte> (Context, ComputeMemoryFlags.ReadOnly | ComputeMemoryFlags.CopyHostPointer, keyspaceJoined);
			var passLenBuffer = new ComputeBuffer<byte> (Context, ComputeMemoryFlags.WriteOnly, 1);
			var flagBuffer = new ComputeBuffer<int> (Context, ComputeMemoryFlags.None, 1);


			Kernel.SetMemoryArgument (0, hashBuffer);
			Kernel.SetMemoryArgument (1, keyspaceBuffer);
			Kernel.SetMemoryArgument (2, resultBuffer);
			Kernel.SetMemoryArgument (3, passLenBuffer);
			Kernel.SetMemoryArgument (4, flagBuffer);

			// execute kernel
			var queue = new ComputeCommandQueue (Context, Device, ComputeCommandQueueFlags.None);

			long firstDim = joinedKeySpace.Count;
			var globalWorksize = new long[] { firstDim, 57 * 57, 57 * 57 };

			queue.Execute (Kernel, new long[] { 0, 0, 0 }, globalWorksize, null, null);

			byte[] passLen = new byte[1];

			queue.ReadFromBuffer (resultBuffer, ref resultData, true, null);
			queue.ReadFromBuffer (passLenBuffer, ref passLen, true, null);

			String password = null;

			if (passLen [0] > 0) {
				logger.Info ("pass len {0}", passLen [0]);
				password = Encoding.ASCII.GetString (resultData, 0, passLen [0]);
				logger.Info ("Found password: \"{0}\"", password);
			} else {
				logger.Info ("Password not found.");
			}

			queue.Finish ();

			return password;
		}
	}
}

