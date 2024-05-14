using System.Diagnostics;
using System.Xml.Linq;
namespace MinMaxApp;

[QueryProperty(nameof(CompartmentID), "compartment")]
public partial class SectionSettings : ContentPage
{
    private int remindersCounter = 0;
    private int medsCounter = 0;


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
        remindersCounter = int.Parse(remAmmount.Text);
        medsCounter = int.Parse(medAmmount.Text);


    }

    private void OnAddDatesClicked(object sender, EventArgs e)
    {
        datesContainer.Children.Clear();  // Clear any existing DatePickers

        if (int.TryParse(remAmmount.Text, out int numberOfDates) && numberOfDates > 0)
        {
            for (int i = 0; i < numberOfDates; i++)
            {
                var datePicker = new TimePicker
                {
                    TextColor = Colors.White,
                    BackgroundColor = Color.Parse("#2B5B54")
                };

                datesContainer.Children.Add(datePicker);
            }
        }
        else
        {
            DisplayAlert("Klaida", "Netinkama įvestis", "Supratau");
        }
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

    private void OnIncreaseClicked(object sender, EventArgs e)
    {
        remindersCounter++;
        remAmmount.Text = remindersCounter.ToString();
        OnAddDatesClicked(remAmmount, new EventArgs());
    }

    private void OnDecreaseClicked(object sender, EventArgs e)
    {
        if (remindersCounter == 1)
            return;

        remindersCounter--;
        remAmmount.Text = remindersCounter.ToString();
        OnAddDatesClicked(remAmmount, new EventArgs());
    }

    private void OnMedsIncreaseClicked(object sender, EventArgs e)
    {
        medsCounter++;
        medAmmount.Text = medsCounter.ToString();
    }

    private void OnMedsDecreaseClicked(object sender, EventArgs e)
    {
        if (medsCounter == 1)
            return;

        medsCounter--;
        medAmmount.Text = medsCounter.ToString();
    }
}