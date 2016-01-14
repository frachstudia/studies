using System;
using Common;

namespace Processor.PasswordMatcher
{
	public interface IPasswordMatcher
	{
		String SearchPassword (byte[] hash, HashType type, int maxLength, String[] keySpace);

		void Abort();
	}
}

