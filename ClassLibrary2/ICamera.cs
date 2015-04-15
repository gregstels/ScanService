namespace Mallenom.ScanNetwork.Core
{
	interface ICamera
	{
		bool IsCamera(string ipAddress, int port);

		string CameraName { get; }
	}
}
