using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Mallenom.ScanNetwork.Core
{
	public class ScanServiceConfigration
	{
		private static readonly IPAddress DefaultMimimum = IPAddress.Parse("192.168.10.1");
		private static readonly IPAddress DefaultMaximum = IPAddress.Parse("192.168.10.20");

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
