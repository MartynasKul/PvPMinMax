namespace MinMaxApp;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();

	}

    private void ReturnToHomePage(object sender, EventArgs e)
    {
        Navigation.PushAsync(new HomePage());
    }
}