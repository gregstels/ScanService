
using System.Net;

namespace Mallenom.ScanNetwork.Core
{
	public class ScanServiceConfigration
	{
		private static readonly IPAddress DefaultMimimum = IPAddress.Parse("192.168.10.190");
		private static readonly IPAddress DefaultMaximum = IPAddress.Parse("192.168.10.200");
	   
		public IPAddress Minimum { get; set; }

		public IPAddress Maximum { get; set; }

		public ScanServiceConfigration()
		{
			SetDefault();
		}

		public void SetDefault()
		{
			Minimum = DefaultMimimum;
			Maximum = DefaultMaximum;
		}
	}
}
