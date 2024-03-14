namespace MinMaxApp;

public partial class SectionSettings : ContentPage
{
	public SectionSettings()
	{
		InitializeComponent();
	}
	private void BackButtonClicked(object sender, EventArgs e) 
	{
		Navigation.PushAsync(new HomePage());
	}
}