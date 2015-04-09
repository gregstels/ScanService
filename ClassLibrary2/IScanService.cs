using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mallenom.ScanNetwork.Core
{
	/// <summary>Представляет методы по сканированию сети.</summary>
    public interface IScanService
	{
		#region Methods

		/// <summary>Сканирует сеть на на личие сетвых устройств.</summary>
		/// <returns>Список обнаруженный устройств.</returns>
        Task<IReadOnlyList<IpAddressData>> ScanNetworkAsync();
        
		
		#endregion
	}
}
