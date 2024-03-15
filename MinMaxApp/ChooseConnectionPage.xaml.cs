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

            //Navigation.PushAsync(new HomePage()); // pereina i homescreen
            await Shell.Current.GoToAsync("//HomePage"); // for now sitaip naviguoti
        }

        private async void bluetoothButton_Clicked(object sender, EventArgs e)
        {
            bluetoothButton.Text = "Bluetooth chosen";

            //Navigation.PushAsync(new HomePage()); // pereina i homescreen
            await Shell.Current.GoToAsync("//HomePage"); // for now sitaip naviguoti
        }
    }

}
