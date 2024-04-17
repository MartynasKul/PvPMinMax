using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net.Http;
using System.Threading.Tasks;

namespace MinMaxApp
{
    internal class ESPController
    {
        private readonly HttpClient _httpClient;
        private readonly string _esp32Url;
        
        public ESPController(string esp32IpAddress)
        {
            string wifiIpAddress = NetworkUtils.GetWifiIpAddress();
            if (wifiIpAddress != null)
            {
                Debug.WriteLine("ABOBA Wi-Fi IP Address: " + wifiIpAddress);
            }
            else
            {
                Debug.WriteLine("ABOBA Failed to retrieve Wi-Fi IP address.");
            }
            _httpClient = new HttpClient();
            //_esp32Url = $"http://{esp32IpAddress}/";
            _esp32Url = wifiIpAddress;
        }

        public async Task<bool> DisplayNumber(int number)
        {
            try
            {
                string endpoint = $"{_esp32Url}/show_number?number={number}";
                //string endpoint = $"http://192.168.4.1/show_number?number={number}";
            
                HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

                // Check if the request was successful (status code 200)
                Debug.WriteLine("ABOBA " + response);
                return response.IsSuccessStatusCode;

                
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ABOBA Error: {ex.Message}");
                return false;
            }
        }
    }
}
