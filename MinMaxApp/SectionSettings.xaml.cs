using Plugin.LocalNotification;
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
            LoadCompartmentInfo(value);
        }
    }

    private int compartmentIdValue;

    LocalDatabase db;


    public SectionSettings()
	{
		InitializeComponent();

        LocalNotificationCenter.Current.NotificationActionTapped += Current_NotificationActionTapped;

        db = new LocalDatabase();

    }


    private void LoadCompartmentInfo(int value)
    {
        compartmentIdValue = value;
        Compartment comp = db.GetCompartment(value);
        MedName.Text = comp.medName;
        medAmmount.Text = comp.amount.ToString();
        remAmmount.Text = comp.reminderAmount.ToString();
    }

    private void UpdateCompartmentInfo()
    {
        Compartment comp = db.GetCompartment(compartmentIdValue);
        MedName.Text = comp.medName;

        db.SetCompartment(compartmentIdValue, MedName.Text, int.Parse(medAmmount.Text), int.Parse(remAmmount.Text));
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

        // Notificationus atitinkamai turbut atnaujint
    }

    private async void SaveButtonClicked(object sender, EventArgs e)
    {
        LocalNotificationCenter.Current.CancelAll();

        Compartment comp = db.GetCompartment(compartmentIdValue);

        DateTime notifyDate = new DateTime();

        if (comp.TimeAmounts.Count > 0)
            notifyDate = DateTime.Now.AddSeconds(comp.TimeAmounts[0].hour * 3600 + comp.TimeAmounts[0].minute * 60);
        else
            notifyDate = DateTime.Now.AddSeconds(15); // 5 min sakykim



        //Notifikaciju pradzia - sukuriamas requestas, ir poto jis parodomas.
        var request = new NotificationRequest
        {
            NotificationId = compartmentIdValue,
            Title = "MinMax",
            Subtitle = $"{compartmentIdValue + 1} skiltis",
            Description = $"Laikas išgerti {MedName.Text}",
            BadgeNumber = 42,
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = notifyDate,
                RepeatType = NotificationRepeat.TimeInterval,
                NotifyRepeatInterval = TimeSpan.FromSeconds(300),
                NotifyAutoCancelTime = DateTime.Now.AddSeconds(600), // Kol kas baigiam siuntinet uz 3h
                // reiktu pagal nustatymus situos sudeliot

            }
        };
        UpdateCompartmentInfo();

        await LocalNotificationCenter.Current.Show(request);

        await Shell.Current.GoToAsync("//HomePage");
        //Navigation.PushAsync(new SectionSettings());
    }

    private void Current_NotificationActionTapped(Plugin.LocalNotification.EventArgs.NotificationActionEventArgs e)
    {
        if (e.IsDismissed)
        {
            LocalNotificationCenter.Current.Cancel();
        }
        else if (e.IsTapped)
        {
            // Decrement amount after tapping notification
            medAmmount.Text = (int.Parse(medAmmount.Text) - 1).ToString();
        }

        throw new NotImplementedException();
    }
}