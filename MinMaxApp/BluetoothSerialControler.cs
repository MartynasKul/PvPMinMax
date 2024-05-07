using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InTheHand.Net.Sockets;
using InTheHand.Net;
using InTheHand.Net.Bluetooth;

namespace MinMaxApp
{
    internal class BluetoothSerialControler
    {
        public async Task<List<BluetoothDeviceInfo>> DiscoverDevicesAsync()
        {
            BluetoothClient bluetoothClient = new BluetoothClient();
            return bluetoothClient.DiscoverDevices().ToList();
        }

        public async Task<BluetoothClient> ConnectToESP32Async(string esp32Name)
        {
            var devices = await DiscoverDevicesAsync();
            var esp32Device = devices.FirstOrDefault(d => d.DeviceName == esp32Name);

            if (esp32Device != null)
            {

                BluetoothClient client = new BluetoothClient();
                Console.WriteLine("RASTAS IRENGINYS: " + esp32Device.DeviceName);
                client.Connect(esp32Device.DeviceAddress, BluetoothService.SerialPort);
                return client;
            }

            return null;
        }

        public void SendNumber(BluetoothClient client, int number)
        {
            if (client != null && client.Connected)
            {
                var stream = client.GetStream();
                // Send the number as a single character
                byte[] data = new byte[] { (byte)number.ToString()[0] };
                Console.WriteLine("SIUNCIAMAS SKAICIUS I ESP:" + number);
                stream.Write(data, 0, data.Length);
            }
        }

    }
}