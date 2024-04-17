namespace MinMaxApp;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();

	}

    private async void ReturnToHomePage(object sender, EventArgs e)
    {
        //Navigation.PushAsync(new HomePage());
        await Shell.Current.GoToAsync("//HomePage");
    }
}