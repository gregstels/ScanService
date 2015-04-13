using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Mallenom.ScanNetwork.Core
{
	public class ScanService : IScanService, IDisposable
	{
		#region Data

		private readonly ScanServiceConfigration _configration;
		private readonly IpScanner _ipScanner;

		#endregion

		#region consts

		private const int ConnectTimeout = 1000;
		private const int RtspPort = 554;
		private const int HttpPort = 80;


		#endregion

		#region .ctor && Dispose

		public ScanService(ScanServiceConfigration configration)
		{
			_configration = configration;
			_ipScanner = new IpScanner();
			
		}

		public void Dispose()
		{
			_ipScanner.Dispose();
		}

		#endregion
	   
		public async Task<IReadOnlyList<IpAddressData>> ScanNetworkAsync()
		{
			var serverList = _ipScanner.Skannig(_configration.Minimum, _configration.Maximum);

			List<IpAddressData> list = null;

			foreach (var ipAddress in serverList)
			{
				var macAddress = string.Empty;
				using(var pProcess = new Process
				{
					StartInfo =
					{
						FileName = "arp",
						Arguments = "-a " + ipAddress,
						UseShellExecute = false,
						RedirectStandardOutput = true,
						CreateNoWindow = true
					}
				})
				{
					pProcess.Start();
					var strOutput = pProcess.StandardOutput.ReadToEnd();
					var substrings = strOutput.Split('-');

					if(substrings.Length >= 8)
					{
						macAddress = substrings[3].Substring(
							Math.Max(0, substrings[3].Length - 2))
									 + "-" + substrings[4]
									 + "-" + substrings[5]
									 + "-" + substrings[6]
									 + "-" + substrings[7]
									 + "-" + substrings[8].Substring(0, 2);
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
							list.Add(new IpAddressData(ipAddress.ToString(), RtspPort, macAddress));

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
							list.Add(new IpAddressData(ipAddress.ToString(), HttpPort, macAddress));
						}

					}
					catch(Exception exc)
					{
						if(exc is ArgumentNullException) throw;
					}
				}
			}

			return list;
		}
	}
}
