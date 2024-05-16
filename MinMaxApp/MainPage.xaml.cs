
using CommunityToolkit.Mvvm.Input;
using GalaSoft.MvvmLight.Command;
using System.Threading.Tasks;


#if ANDROID
using Android.Content;
using Android.Locations;

#elif IOS || MACCATALYST

using CoreLocation;

#elif WINDOWS
using Windows.Devices.Geolocation;

#endif


namespace MinMaxApp
{
    public partial class MainPage : ContentPage
    {
        [RelayCommand]
        async Task RequestBluetooth()
        {
            var status = PermissionStatus.Unknown;

            status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (status == PermissionStatus.Granted) 
            {
                return;
            }

            if (Permissions.ShouldShowRationale<Permissions.LocationWhenInUse>()) 
            {
                await Shell.Current.DisplayAlert("Permission required", "To use bluetooth our app needs location settings", "ok");
            }

            if (status != PermissionStatus.Granted)
            {
                await Shell.Current.DisplayAlert("Permission required",
                    "Location permission is required for bluetooth scanning." +
                    "Our Program does not store or use your location for other purposes",
                    "ok");
            }
        }

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new ChooseConnectionPage());
            //await Shell.Current.GoToAsync("//ChooseConnectionPage"); // for now sitaip naviguoti einam kiauriai i bluetooth prisijungima
            await Shell.Current.GoToAsync("//BluetoothConnectionScreen");
        }
        private async void HomeClicked(object sender, EventArgs e)
        {
            // Navigation.PushAsync(new HomePage()); // pereina i homescreen 
            await Shell.Current.GoToAsync("//HomePage"); // for now sitaip naviguoti
        }
    }

}
