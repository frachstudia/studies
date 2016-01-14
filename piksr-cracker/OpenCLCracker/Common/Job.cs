using System;

namespace Common
{
	public enum HashType
	{
		MD5,
		SHA1
	};

	public enum Status
	{
		NONE,
		QUEUED,
		PROCESSING,
		SOLVED,
		PASSWORD_NOT_FOUND,
		ABORTED
	};

	public class Job
	{
		public int Id { get; set; }

		public HashType Type { get; set; }

		public byte[] Hash { get; set; }

		public Status Status { get; set; }

		public String[] KeySpace { get; set; }

		public int MaxLength { get; set; }

		public String Password { get; set; }

		public Job ()
		{
			Type = HashType.MD5;
			Hash = new byte[8];
			Status = Status.NONE;
			MaxLength = 5;
			Password = null;
		}
			
		public override string ToString ()
		{
			return string.Format ("[{0} Job: Id={1}, {2}: {3}]",
				Status, Id, Type, BitConverter.ToString (Hash).Replace ("-", "").ToLower ());
		}
	}
}

