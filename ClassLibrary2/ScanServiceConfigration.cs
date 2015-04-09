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
		private const string DefaultMimimum = "";
		private const string DefaultMaximum = "";

		public string Minimum { get; set; }

		public string Maximum { get; set; }

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
