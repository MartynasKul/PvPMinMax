using InTheHand.Net.Sockets;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MinMaxApp
{
    public partial class BluetoothConnectionScreen : ContentPage
    {

        // Cancellinima pairinimo jei reiks, pridetas bus

        private bool isSearching = false;

        public BluetoothConnectionScreen()
        {
            InitializeComponent();
            RequestBluetooth();
        }

        private async void bluetoothButton_Clicked(object sender, EventArgs e)
        {
            if (isSearching)
            {
                resetLabels();
                return;
            }

            isSearching = true;

            descriptionLabel.Text = "Pairing in progress...";

            loadingIndicator.IsVisible = true;
            loadingIndicator.IsRunning = true;
            bluetoothButton.IsVisible = false;

            Debug.WriteLine("Searching for devices");

            // Simulate a delay for the Bluetooth connection attempt
            BluetoothClient bluetoothClient = await Task.Run(() => BluetoothManager.Instance.GetBluetoothClient());

            // Check if the Bluetooth connection was successful
            if (bluetoothClient == null || !bluetoothClient.Connected)
            {
                // Handle the failure to connect, e.g., display an error message
                await DisplayAlert("Error", "Failed to connect to the Bluetooth device.", "OK");
                resetLabels();
                bluetoothButton.IsVisible = true;
                descriptionLabel.Text = "Start pairing by clicking the button below";
                return; // Early return since we couldn't establish a connection
            }
            else
            {
                Debug.WriteLine("DEVICE FOUND");
                descriptionLabel.Text = "Device paired successfully, moving on...";
                bluetoothButton.IsVisible = false;
                resetLabels();
                successIndicator.Opacity = 0;
                successIndicator.IsVisible = true; // Show the success indicator
                await successIndicator.FadeTo(1, 2000);
                BluetoothManager.SetBTConnectionState(true);
 



                // Navigate to the home page
                await Shell.Current.GoToAsync("//HomePage");
            }
        }

        private void resetLabels()
        {
            isSearching = false;
            loadingIndicator.IsVisible = false;
            loadingIndicator.IsRunning = false;
            bluetoothButton.Text = "Connect";
            // If a task is running, you can cancel it here if needed
        }

        async Task RequestBluetooth()
        {
            if (DeviceInfo.Platform != DevicePlatform.Android)
                return;

            var status = PermissionStatus.Unknown;

            if (DeviceInfo.Version.Major >= 12)
            {
                status = await Permissions.CheckStatusAsync<Permissions.Bluetooth>();

                if (status == PermissionStatus.Granted)
                    return;

                if (Permissions.ShouldShowRationale<Permissions.Bluetooth>())
                {
                    await Shell.Current.DisplayAlert("Needs permissions", "BECAUSE!!!", "OK");
                }

                status = await Permissions.RequestAsync<Permissions.Bluetooth>();


            }
            else
            {
                status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

                if (status == PermissionStatus.Granted)
                    return;

                if (Permissions.ShouldShowRationale<Permissions.LocationWhenInUse>())
                {
                    await Shell.Current.DisplayAlert("Needs permissions", "BECAUSE!!!", "OK");
                }

                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();


            }


            if (status != PermissionStatus.Granted)
                await Shell.Current.DisplayAlert("Permission required",
                    "Location permission is required for bluetooth scanning. " +
                    "We do not store or use your location at all.", "OK");
        }
    }
}