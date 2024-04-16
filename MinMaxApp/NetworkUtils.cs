using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;


namespace MinMaxApp
{
    internal class NetworkUtils
    {
        public static string GetWifiIpAddress()
        {
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            // Find the Wi-Fi network interface that is currently connected
            NetworkInterface wifiInterface = networkInterfaces.FirstOrDefault(
                x => x.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 &&
                     x.OperationalStatus == OperationalStatus.Up);

            if (wifiInterface != null)
            {
                // Get the IPv4 address of the Wi-Fi network interface
                foreach (UnicastIPAddressInformation ipInfo in wifiInterface.GetIPProperties().UnicastAddresses)
                {
                    if (ipInfo.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        return ipInfo.Address.ToString();
                    }
                }
            }

            return null;
        }

    }
}
