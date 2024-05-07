using InTheHand.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinMaxApp
{
    internal class BluetoothManager
    {
        private static BluetoothManager _instance;
        public static BluetoothManager Instance => _instance ??= new BluetoothManager();

        private BluetoothSerialControler _bluetoothController;
        private BluetoothClient _bluetoothClient;

        private BluetoothManager()
        {
            _bluetoothController = new BluetoothSerialControler();
        }

        public async Task<BluetoothClient> GetBluetoothClient()
        {
            if (_bluetoothClient == null || !_bluetoothClient.Connected)
            {
                _bluetoothClient = await _bluetoothController.ConnectToESP32Async("MinMaxBT");
            }
            return _bluetoothClient;
        }
    }
}
