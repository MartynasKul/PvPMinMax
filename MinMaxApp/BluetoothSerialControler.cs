using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InTheHand.Net.Sockets;
using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using System.Diagnostics;

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
                string numberString = "N:" + number.ToString();
                byte[] data = Encoding.ASCII.GetBytes(numberString);
                stream.Write(data, 0, data.Length);
            }
        }

        public void SendCurrentTime(BluetoothClient client)
        {
            if (client != null && client.Connected)
            {
                var stream = client.GetStream();
                DateTime now = DateTime.Now;
                string timeString = "T:" + now.ToString("yyyy-MM-dd HH:mm:ss");
                byte[] data = Encoding.ASCII.GetBytes(timeString);
                stream.Write(data, 0, data.Length);
            }
        }
        public void SendNumberAndReminderTime(BluetoothClient client, int number, DateTime reminderTime)
        {
            if (client != null && client.Connected)
            {
                var stream = client.GetStream();
                string message = $"RN:{number},{reminderTime.ToString("yyyy-MM-dd HH:mm:ss")}\n";
                byte[] data = Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
        }

        public void SendSection(BluetoothClient client, int section)
        {
            if (client != null && client.Connected)
            {
                var stream = client.GetStream();
                string sectionMessage = $"S:{section}\n";  // Assuming 'S' is the identifier for section
                byte[] data = Encoding.ASCII.GetBytes(sectionMessage);
                stream.Write(data, 0, data.Length);
            }
        }

        public void StartBreathingEffect(BluetoothClient client, int section)
        {
            if (client != null && client.Connected)
            {
                var stream = client.GetStream();
                string startBreathingMessage = $"B:{section}\n";  // Assuming 'B' is the identifier for start breathing
                byte[] data = Encoding.ASCII.GetBytes(startBreathingMessage);
                stream.Write(data, 0, data.Length);
                Console.WriteLine("PALEIDZIAMAS EFEKTAS SEKCIJOJE: " + section);
            }
        }

        public void StopBreathingEffect(BluetoothClient client, int section)
        {
            if (client != null && client.Connected)
            {
                var stream = client.GetStream();
                string stopBreathingMessage = $"E:{section}\n";  // Assuming 'E' is the identifier for stop breathing
                byte[] data = Encoding.ASCII.GetBytes(stopBreathingMessage);
                stream.Write(data, 0, data.Length);
                Console.WriteLine("SUSTEBDOMAS EFEKTAS SEKCIJOJE: " + section);
            }
        }
    }
}