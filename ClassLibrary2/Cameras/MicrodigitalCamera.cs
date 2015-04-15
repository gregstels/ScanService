using System;
using System.Text;

namespace Mallenom.ScanNetwork.Core.Cameras
{
	sealed class MicrodigitaCamera : CameraBase, ICamera
	{
		public bool IsCamera(string ipAddress, int port)
		{
			var builder = new StringBuilder();

			builder.AppendFormat("http://{0}:{1}/", ipAddress, port);
			builder.Append("cgi-bin/");
			builder.AppendFormat("fwsysget.cgi?FwModId={0}&FwCgiVer={1}", "0", "0x0001");

			var uri = new Uri(builder.ToString());

			var message = ExecuteHttpResponse(uri);

			if(message.Contains("Model"))
			{
				return true;
			}

			return false;
		}
	}
}
