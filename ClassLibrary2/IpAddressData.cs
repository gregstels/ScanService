﻿namespace Mallenom.ScanNetwork.Core
{
	public class IpAddressData
	{
		#region Data

		private readonly string _address;
		private readonly int _port;
		private readonly string _physicalAddress;

		#endregion

		#region .ctor

		/// <summary>Создание <see cref="IpAddressData"/>.</summary>
		/// <param name="address">Адрес.</param>
		/// <param name="port">Порт.</param>
		/// <param name="physicalAddress">MAC address.</param>
		public IpAddressData(string address, int port, string physicalAddress)
		{
			_address = address;
			_port = port;
			_physicalAddress = physicalAddress;

		}

		#endregion

		#region Propeties

		/// <summary>Возвращает адрес.</summary>
		/// <value>Адрес.</value>
		public string Address
		{
			get { return _address; }
		}

		/// <summary>Возвращает порт.</summary>
		/// <value>Порт.</value>
		public int Port
		{
			get { return _port; }
		}

		public string PhysicalAddress
		{
			get { return _physicalAddress;}
		}

		#endregion
	}
}
