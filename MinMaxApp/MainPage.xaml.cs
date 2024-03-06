namespace MinMaxApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChooseConnectionPage());
        }
        private void HomeClicked(object sender, EventArgs e) 
        {
            Navigation.PushAsync(new HomePage()); // pereina i homescreen
        }
    }

}
