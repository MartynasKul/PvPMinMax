namespace MinMaxApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new ChooseConnectionPage());
            await Shell.Current.GoToAsync("//ChooseConnectionPage"); // for now sitaip naviguoti
        }
        private async void HomeClicked(object sender, EventArgs e)
        {
            // Navigation.PushAsync(new HomePage()); // pereina i homescreen 
            await Shell.Current.GoToAsync("//HomePage"); // for now sitaip naviguoti
        }
    }

}
