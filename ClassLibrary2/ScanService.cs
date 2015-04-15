using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using Mallenom.ScanNetwork.Core.Cameras;


namespace Mallenom.ScanNetwork.Core
{
	public class ScanService : IScanService, IDisposable
	{
		#region Data

		private readonly ScanServiceConfigration _configration;
		private readonly IpScanner _ipScanner;
		private readonly IReadOnlyList<ICamera> _cameras;

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

			_cameras = new[]
			{
				new MicrodigitalCamera(),
			};
		}

		public void Dispose()
		{
			_ipScanner.Dispose();
		}

		#endregion

		public async Task<IReadOnlyList<CameraData>> ScanNetworkAsync()
		{
			var serverList = _ipScanner.Skannig(_configration.Minimum, _configration.Maximum);

			var hostName = Dns.GetHostName();
			var hostEntry = Dns.GetHostEntry(hostName);
			var localIpV4Address = hostEntry.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);

			if(localIpV4Address == null)
			{
				throw new InvalidOperationException("Не найден IP адрес локальной машины по имени хоста локальной машины.");
			}

			var localIpAddress = localIpV4Address.ToString();

			List<IpAddressData> avaibleIpAddressDataList = null;
			List<CameraData> cameraDatas = null;
			foreach(var ipAddress in serverList)
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

					var currentIpAddress = ipAddress.ToString();

					if(string.Compare(currentIpAddress, localIpAddress, CultureInfo.InvariantCulture, CompareOptions.None) == 0)
					{
						var allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
						foreach(var networkInterface in allNetworkInterfaces)
						{
							if(macAddress == String.Empty)
							{
								macAddress = networkInterface.GetPhysicalAddress().ToString();
							}
						}

					}

					try
					{
						using(var tcpClient = new TcpClient
						{
							SendTimeout = ConnectTimeout
						})
						{
							if(avaibleIpAddressDataList == null)
							{
								avaibleIpAddressDataList = new List<IpAddressData>();
							}

							await tcpClient.ConnectAsync(ipAddress, HttpPort)
								.ConfigureAwait(continueOnCapturedContext: false);
							avaibleIpAddressDataList.Add(new IpAddressData(ipAddress.ToString(), HttpPort, macAddress));
						}

					}
					catch(Exception exc)
					{
						if(exc is ArgumentNullException) throw;
					}
				}
			}

			if(avaibleIpAddressDataList == null)
			{
				return null;
			}

			cameraDatas = new List<CameraData>();

			foreach(var ipAddress in avaibleIpAddressDataList)
			{
				foreach(var camera in _cameras)
				{
					if(camera.IsCamera(ipAddress.Address, ipAddress.Port))
					{
						cameraDatas.Add(new CameraData(ipAddress.Address, ipAddress.Port, ipAddress.PhysicalAddress, camera.CameraName));
					}
				}
			}

			return cameraDatas;
		}
	}
}
