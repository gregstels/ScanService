using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace Mallenom.ScanNetwork.Core
{
	/// <summary>Сканер IP адресов сети.</summary>
	public sealed class IpScanner : IDisposable
	{
		#region consts
		
		private const int Timeout = 500;
		private const IPStatus Success = IPStatus.Success;
		private const string SendMessage = "ping";

		#endregion

		#region statics

		private static readonly PingOptions PingOptions = new PingOptions(64, true);
		private static readonly byte[] Buffer = Encoding.ASCII.GetBytes(SendMessage);

		#endregion

		#region Data

		private readonly Ping _ping;

		#endregion

		#region .ctor && Dispose

		/// <summary>Создание <see cref="IpScanner"/>.</summary>
		public IpScanner()
		{
			_ping = new Ping();
		}

		public void Dispose()
		{
			_ping.Dispose();
		}

		#endregion

		#region Methods

		/// <summary>Сканировать сеть.</summary>
		/// <returns>Список доступных для ICMP связи адресов.</returns>
		public IReadOnlyList<IPAddress> Skannig(IPAddress minimum, IPAddress maximum)
		{
			var range = IpAddressesRange(minimum, maximum);

			var stopwatch = new Stopwatch();
			stopwatch.Start();

			var list = new List<IPAddress>(100);
			foreach(var address in range)
			{
				var reaply = _ping.Send(
					address,
					Timeout,
					Buffer,
					PingOptions);

				if(reaply != null)
				{
					if(reaply.Status == Success)
					{
						if(reaply.Address != null)
						{
							list.Add(reaply.Address);
						}
					}
				}
			}

			stopwatch.Stop();
			var message = string.Format(
					CultureInfo.InvariantCulture,
					"Время обработки списка адресов: {0}.",
					stopwatch.Elapsed);
			Debug.WriteLine(message);

			return list;

		}

		private static IEnumerable<IPAddress> IpAddressesRange(IPAddress minimum, IPAddress maximum)
		{
			var firstIpAddressAsBytesArray = minimum.GetAddressBytes();
			var lastIpAddressAsBytesArray = maximum.GetAddressBytes();

			Array.Reverse(firstIpAddressAsBytesArray);
			Array.Reverse(lastIpAddressAsBytesArray);

			var firstIpAddressAsInt = BitConverter.ToInt32(firstIpAddressAsBytesArray, 0);
			var lastIpAddressAsInt = BitConverter.ToInt32(lastIpAddressAsBytesArray, 0);

			var ipAddressesInTheRange = new List<IPAddress>();
			var stopwatch = new Stopwatch();
			stopwatch.Start();
			for (var i = firstIpAddressAsInt; i <= lastIpAddressAsInt; i++)
			{
				var bytes = BitConverter.GetBytes(i);
				var address = new IPAddress(new[] {bytes[3], bytes[2], bytes[1], bytes[0]});
				ipAddressesInTheRange.Add(address);				
			}
			stopwatch.Stop();
			var message = string.Format(
					CultureInfo.InvariantCulture,
					"Время составления списка адресов: {0}.",
					stopwatch.Elapsed);
			Debug.WriteLine(message);

			return ipAddressesInTheRange;
		}
		#endregion
	}
}
