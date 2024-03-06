namespace MinMaxApp;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

	private void OpenSettings(object sender, EventArgs e) 
	{
        Navigation.PushAsync(new Settings()); // pereina i homescreen
    }
	private void OnMed1Clicked(object sender, EventArgs e) 
	{
	    Med1Button.Text = "Med1 Pressed";
        Navigation.PushAsync(new SectionSettings());
	}
    private void OnMed2Clicked(object sender, EventArgs e)
    {
        Med2Button.Text = "Med2 Pressed";
        Navigation.PushAsync(new SectionSettings());
    }
    private void OnMed3Clicked(object sender, EventArgs e)
    {
        Med3Button.Text = "Med3 Pressed";
        Navigation.PushAsync(new SectionSettings());
    }
    private void OnMed4Clicked(object sender, EventArgs e)
    {
        Med4Button.Text = "Med4 Pressed";
        Navigation.PushAsync(new SectionSettings());
    }
    private void OnMed5Clicked(object sender, EventArgs e)
    {
        Med5Button.Text = "Med5 Pressed";
        Navigation.PushAsync(new SectionSettings());
    }
    private void OnMed6Clicked(object sender, EventArgs e)
    {
        Med6Button.Text = "Med6 Pressed";
        Navigation.PushAsync(new SectionSettings());
    }
    private void OnMed7Clicked(object sender, EventArgs e)
    {
        Med7Button.Text = "Med7 Pressed";
        Navigation.PushAsync(new SectionSettings());
    }
    private void OnMed8Clicked(object sender, EventArgs e)
    {
        Med8Button.Text = "Med8 Pressed";
        Navigation.PushAsync(new SectionSettings());
    }



}