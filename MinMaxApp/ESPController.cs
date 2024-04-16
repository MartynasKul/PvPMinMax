using System;
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
            _httpClient = new HttpClient();
            //_esp32Url = $"http://{esp32IpAddress}/";
            _esp32Url = "http://192.168.4.1";
        }

        public async Task<bool> DisplayNumber(int number)
        {
            try
            {
                string endpoint = $"{_esp32Url}/show_number?number={number}";
                HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

                // Check if the request was successful (status code 200)
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}
