using System;
using System.Collections.Generic;
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
			var list = new List<string>(100);

			for(var i = 192; i <= 192; i++)
				for(var j = 168; j <= 168; j++)
					for(var k = 10; k <= 10; k++)
						for(var s = 0; s <= 255; s++)
						{
							var address = IPAddress
								.Parse(
									i.ToString(CultureInfo.InvariantCulture) + '.' +
									j.ToString(CultureInfo.InvariantCulture) + '.' +
									k.ToString(CultureInfo.InvariantCulture) + '.' +
									s.ToString(CultureInfo.InvariantCulture));

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

		#endregion
	}
}
