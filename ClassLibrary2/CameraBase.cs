using System;
using System.IO;
using System.Net;
using System.Text;

namespace Mallenom.ScanNetwork.Core
{
	internal abstract class CameraBase
	{
		private const int Timeout = 1000;

		protected virtual string ExecuteHttpResponse(Uri uri)
		{
			var request = WebRequest.Create(uri);

			request.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
			request.Proxy = null;
			request.Timeout = Timeout;
			
			try
			{
				using(var response = request.GetResponse())
				{
					using(var stream = response.GetResponseStream())
					{
						if(stream == null) return null;
						using(var reader = new StreamReader(stream, Encoding.UTF8))
						{
							var responseString = reader.ReadToEnd();
							return responseString;
						}
					}
				}
			}
			catch(WebException exc)
			{
				return exc.Message;
			}
		}
	}
}
