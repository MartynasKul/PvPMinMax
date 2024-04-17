using Microsoft.Maui.Controls.PlatformConfiguration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;




namespace MinMaxApp
{
    internal class NetworkUtils
    {
        
        public static string GetWifiIpAddress()
        {
            var addresses = Dns.GetHostEntry((Dns.GetHostName()))
                    .AddressList
                    .Where(x => x.AddressFamily == AddressFamily.InterNetwork)
                    .Select(x => x.ToString())
                    .ToArray();
            foreach (var address in addresses) 
            {
                Debug.WriteLine("ABOBA " + address.ToString());
                
            
            }
            Debug.WriteLine(System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable());
            Debug.WriteLine("ABOBA");
            string hostName = Dns.GetHostName();
            Debug.WriteLine(hostName);

            // Get the IP from GetHostByName method of dns class. 
            string IP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
            Debug.WriteLine("IP Address is : " + IP);




            Debug.WriteLine("ABOBA");

            NetworkAccess accessType = Connectivity.Current.NetworkAccess;

            if (accessType == NetworkAccess.Internet)
            {
                Debug.WriteLine(accessType);
            }

            IEnumerable<ConnectionProfile> profiles = Connectivity.Current.ConnectionProfiles;

            if (profiles.Contains(ConnectionProfile.WiFi))
            {
                foreach (var profile in profiles) 
                {

                    Debug.WriteLine("ABOBA " + profile.ToString());
                }
            }



            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            //foreach (NetworkInterface networkInterface in networkInterfaces) 
            //{
            //    Debug.WriteLine(networkInterface.ToString());
            //    if (networkInterface.Supports(NetworkInterfaceComponent.IPv4)) 
            //    {
            //        Debug.WriteLine("ABOBA"+networkInterface.GetPhysicalAddress());
            //    }
            //}

            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                Debug.WriteLine("ABOBA" + ni.Name + " " + ni.GetType());

                IPInterfaceProperties properties = ni.GetIPProperties();
                Debug.WriteLine(ni.Description);
                Debug.WriteLine(String.Empty.PadLeft(ni.Description.Length, '='));
                Debug.WriteLine("  Interface type .......................... : {0}", ni.NetworkInterfaceType);
                Debug.WriteLine("  Physical Address ........................ : {0}",
                        ni.GetPhysicalAddress().ToString());
              
                Debug.WriteLine("  Is receive only.......................... : {0}", ni.IsReceiveOnly);
                Debug.WriteLine("  Multicast................................ : {0}", ni.SupportsMulticast);
               


                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    Debug.WriteLine("ABOBA" + ni.Name);
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        { 
                            Debug.WriteLine("ABOBA " + ip.Address.ToString());
                        }
                    }
                }
            }

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
