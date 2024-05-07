using InTheHand.Net.Sockets;
using System.Windows.Input;


namespace MinMaxApp
{
    public partial class ChooseConnectionPage : ContentPage
    {
        public ChooseConnectionPage()
        {
            InitializeComponent();
        }

        private void GoBack(object sender, EventArgs e)
        {
            Navigation.PopAsync(); // Move to the previous page in the nav stack
        }

        private async void wifiButton_Clicked(object sender, EventArgs e)
        {
            wifiButton.Text = "Wi-Fi chosen";
            // Initialize Bluetooth connection
            
            //Navigation.PushAsync(new HomePage()); // pereina i homescreen
            await Shell.Current.GoToAsync("//HomePage"); // for now sitaip naviguoti
        }

        private async void bluetoothButton_Clicked(object sender, EventArgs e)
        {
            bluetoothButton.Text = "Bluetooth chosen";

            // Initialize Bluetooth connection using the BluetoothManager singleton
            BluetoothClient bluetoothClient = await BluetoothManager.Instance.GetBluetoothClient();

            // Check if the Bluetooth connection was successful
            if (bluetoothClient == null || !bluetoothClient.Connected)
            {
                // Handle the failure to connect, e.g., display an error message
                await DisplayAlert("Error", "Failed to connect to the Bluetooth device.", "OK");
                return; // Early return since we couldn't establish a connection
            }

            //Navigation.PushAsync(new HomePage()); // pereina i homescreen
            await Shell.Current.GoToAsync("//HomePage"); // for now sitaip naviguoti
        }
    }
}
