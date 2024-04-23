using System.Diagnostics;
using System.Xml.Linq;
namespace MinMaxApp;

[QueryProperty(nameof(CompartmentID), "compartment")]
public partial class SectionSettings : ContentPage
{
    public int CompartmentID
    {
        set
        {
            DoTheThing(value);
        }
    }

    private int compartmentIdValue;

	public SectionSettings()
	{
		InitializeComponent();

    }


    private void DoTheThing(int value)
    {
        compartmentIdValue = value;
        LocalDatabase db = new LocalDatabase();
        Compartment comp = db.GetCompartment(value);
        MedName.Text = comp.medName;
        medAmmount.Text = comp.amount.ToString();
        remAmmount.Text = comp.reminderAmount.ToString();
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
        LocalDatabase db = new LocalDatabase();
        db.SetCompartment(compartmentIdValue, MedName.Text, int.Parse(medAmmount.Text), int.Parse(remAmmount.Text));

        await Shell.Current.GoToAsync("//HomePage");
        //Navigation.PushAsync(new SectionSettings());
    }
}