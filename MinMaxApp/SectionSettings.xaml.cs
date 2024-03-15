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
    private void MedAmmountChanged(object sender, ValueChangedEventArgs e)
    {
        int val = Convert.ToInt32(e.NewValue);
        medAmmount.Text = val.ToString();
    }
    private void ReminderAmmountChanged(Object sender, ValueChangedEventArgs e)
    {
        int val = Convert.ToInt32(e.NewValue);
        remAmmount.Text = val.ToString();
    }

    private async void SaveButtonClicked(object sender, EventArgs e)
    {


        await Shell.Current.GoToAsync("//HomePage");
        //Navigation.PushAsync(new SectionSettings());
    }
}