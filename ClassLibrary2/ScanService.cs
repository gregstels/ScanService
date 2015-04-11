using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mallenom.ScanNetwork.Core
{
	public class ScanService : IScanService
	{
		#region Data

		private readonly ScanServiceConfigration _configration;
	    private readonly IpScanner _ipScanner;
		//private readonly IEnumerable<ICamera> _cameras;

		#endregion

		#region consts

		private const int ConnectTimeout = 1000;
		private const int RtspPort = 554;
		private const int HttpPort = 80;


		#endregion

		#region .ctor

        public ScanService(ScanServiceConfigration configration, IpScanner ipScanner)
		{
		    _configration = configration;
            _ipScanner = ipScanner;
            
		}

	    #endregion
       
		public async Task<IReadOnlyList<IpAddressData>> ScanNetworkAsync()
		{
            IReadOnlyList<string> serverList = _ipScanner.Skannig();
		    List<IpAddressData> list = null;
            foreach (var ipAddress in serverList)
			{
             
				var macAddress = string.Empty;
				var pProcess = new Process
				{
					StartInfo =
					{
						FileName = "arp",
						Arguments = "-a " + ipAddress,
						UseShellExecute = false,
						RedirectStandardOutput = true,
						CreateNoWindow = true
					}
				};
				pProcess.Start();
				var strOutput = pProcess.StandardOutput.ReadToEnd();
				var substrings = strOutput.Split('-');

				if(substrings.Length >= 8)
				{
					macAddress = substrings[3].Substring(Math.Max(0, substrings[3].Length - 2))
					             + "-" + substrings[4] + "-" + substrings[5] + "-" + substrings[6]
					             + "-" + substrings[7] + "-"
					             + substrings[8].Substring(0, 2);


				}


				try
				{
					using(var tcpClient = new TcpClient
					{
						SendTimeout = ConnectTimeout
					})
					{
						if(list == null)
						{
							list = new List<IpAddressData>();
						}

						await tcpClient.ConnectAsync(ipAddress, RtspPort)
							.ConfigureAwait(continueOnCapturedContext: false);
						list.Add(new IpAddressData(ipAddress, RtspPort, macAddress));

					}
				}
				catch(Exception exc)
				{
					if(exc is ArgumentNullException) throw;
				}
				try
				{
					using(var tcpClient = new TcpClient
					{
						SendTimeout = ConnectTimeout
					})
					{
						if(list == null)
						{
							list = new List<IpAddressData>();
						}

						await tcpClient.ConnectAsync(ipAddress, HttpPort)
							.ConfigureAwait(continueOnCapturedContext: false);
						list.Add(new IpAddressData(ipAddress, HttpPort, macAddress));
					}

				}
				catch(Exception exc)
				{
					if(exc is ArgumentNullException) throw;
				}
			}


			/*foreach(var ipAddress in list)
			{
				foreach(var camera in _cameras)
				{
					if(camera.IsCamera(ipAddress.Address, ipAddress.Port))
					{
						//progress.Report(ipAddress);
						//avaibleCameraAddress.Add(ipAddress);
					}
				}
			}
            */
			return list;

		}
	}
}
