namespace Mallenom.ScanNetwork.Core
{
	public class IpAddressData
	{
		#region Data

		private readonly string _address;
		private readonly int _port;
        private readonly string _macadress;

		#endregion

		#region .ctor

		/// <summary>Создание <see cref="IpAddressData"/>.</summary>
		/// <param name="address">Адрес.</param>
		/// <param name="port">Порт.</param>
		public IpAddressData(string address, int port, string macadress)
		{
			_address = address;
			_port = port;
            _macadress = macadress;

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

        public string Macadress
        {
            get { return _macadress;}
        }

		#endregion
	}
}
