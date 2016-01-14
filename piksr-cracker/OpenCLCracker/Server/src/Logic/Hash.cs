using System;
using System.Runtime.Serialization;

namespace Server.Logic
{
	public class Hash
	{
		public string id { get; set; }

		public string hash { get; set; }

		public string sum { get; set; }			// md5 or sha1

		public int maxLength { get; set; }
	}
}

