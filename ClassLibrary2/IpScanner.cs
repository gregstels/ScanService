using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;

namespace Mallenom.ScanNetwork.Core
{
	/// <summary>Сканер IP адресов сети.</summary>
	public sealed class IpScanner : IDisposable
	{
		#region consts
		
		private const int Timeout = 250;
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
		public IReadOnlyList<string> Skannig()
		{

            var firstadAddress = IPAddress.Parse("192.168.100.0");
            var lasAddress = IPAddress.Parse("192.168.100.10");
            var range = IPAddressesRange(firstadAddress, lasAddress);
		    
            
            var list = new List<string>(100);
            foreach (var address in range)
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
										list.Add(reaply.Address.ToString());
                                    }
								}
							}
						}
            
			return list;

		}

        private static List<IPAddress> IPAddressesRange(IPAddress firstadAddress, IPAddress lasAddress)
        {
            var firstIPAddressAsBytesArray = firstadAddress.GetAddressBytes();
            var lastIPAddressAsBytesArray = lasAddress.GetAddressBytes();
            Array.Reverse(firstIPAddressAsBytesArray);
            Array.Reverse(lastIPAddressAsBytesArray);
            var firstIPAddressAsInt = BitConverter.ToInt32(firstIPAddressAsBytesArray, 0);
            var lastIPAddressAsInt = BitConverter.ToInt32(lastIPAddressAsBytesArray, 0);
            var ipAddressesInTheRange = new List<IPAddress>();
            for (var i = firstIPAddressAsInt; i <= lastIPAddressAsInt; i++)
            {
                var bytes = BitConverter.GetBytes(i);
                var newIp = new IPAddress(new[] {bytes[3], bytes[2], bytes[1], bytes[0]});
                ipAddressesInTheRange.Add(newIp);
            }
            return ipAddressesInTheRange;
        }
		#endregion
	}
}
