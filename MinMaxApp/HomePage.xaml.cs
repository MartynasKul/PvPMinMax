namespace MinMaxApp;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private void OpenSettings(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SettingsPage()); // pereina i homescreen
    }



    private async void OnMed1Clicked(object sender, EventArgs e)
    {
        string buttonId = "b1";
        string originalName = Med1Button.Text;

        Med1Button.Text = originalName + " Pressed";

        //await Shell.Current.GoToAsync($"//SectionSettings?buttonId={buttonId}&originalButtonName={originalName}");
        await Shell.Current.GoToAsync("//SectionSettings");
        //Navigation.PushAsync(new SectionSettings());
    }
    private async void OnMed2Clicked(object sender, EventArgs e)
    {
        string buttonId = "b2";
        string originalName = Med2Button.Text;

        Med2Button.Text = originalName + " Pressed";

        //await Shell.Current.GoToAsync($"//SectionSettings?buttonId={buttonId}&originalButtonName={originalName}");
        await Shell.Current.GoToAsync("//SectionSettings");
        //Navigation.PushAsync(new SectionSettings());
    }
    private async void OnMed3Clicked(object sender, EventArgs e)
    {
        string buttonId = "b3";
        string originalName = Med3Button.Text;

        Med3Button.Text = originalName + " Pressed";

        //await Shell.Current.GoToAsync($"//SectionSettings?buttonId={buttonId}&originalButtonName={originalName}");
        await Shell.Current.GoToAsync("//SectionSettings");
        //Navigation.PushAsync(new SectionSettings());
    }
    private async void OnMed4Clicked(object sender, EventArgs e)
    {
        string buttonId = "b4";
        string originalName = Med4Button.Text;

        Med4Button.Text = originalName + " Pressed";

        //await Shell.Current.GoToAsync($"//SectionSettings?buttonId={buttonId}&originalButtonName={originalName}");
        await Shell.Current.GoToAsync("//SectionSettings");
        //Navigation.PushAsync(new SectionSettings());
    }
    private async void OnMed5Clicked(object sender, EventArgs e)
    {
        string buttonId = "b5";
        string originalName = Med5Button.Text;

        Med5Button.Text = originalName + " Pressed";

        //await Shell.Current.GoToAsync($"//SectionSettings?buttonId={buttonId}&originalButtonName={originalName}");
        await Shell.Current.GoToAsync("//SectionSettings");
        //Navigation.PushAsync(new SectionSettings());
    }
    private async void OnMed6Clicked(object sender, EventArgs e)
    {
        string buttonId = "b6";
        string originalName = Med6Button.Text;

        Med6Button.Text = originalName + " Pressed";

        //await Shell.Current.GoToAsync($"//SectionSettings?buttonId={buttonId}&originalButtonName={originalName}");
        await Shell.Current.GoToAsync("//SectionSettings");
        //Navigation.PushAsync(new SectionSettings());
    }
    private async void OnMed7Clicked(object sender, EventArgs e)
    {
        string buttonId = "b7";
        string originalName = Med7Button.Text;

        Med7Button.Text = originalName + " Pressed";

        //await Shell.Current.GoToAsync($"//SectionSettings?buttonId={buttonId}&originalButtonName={originalName}");
        await Shell.Current.GoToAsync("//SectionSettings");
        //Navigation.PushAsync(new SectionSettings());
    }
    private async void OnMed8Clicked(object sender, EventArgs e)
    {
        string buttonId = "b8";
        string originalName = Med8Button.Text;

        Med8Button.Text = originalName + " Pressed";

        //await Shell.Current.GoToAsync($"//SectionSettings?buttonId={buttonId}&originalButtonName={originalName}");
        await Shell.Current.GoToAsync("//SectionSettings");
        //Navigation.PushAsync(new SectionSettings());
    }



}