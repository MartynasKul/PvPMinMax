
using Plugin.LocalNotification;
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
            LoadCompartmentInfo(value);
        }
    }

    private int compartmentIdValue;

    LocalDatabase db;

    Compartment compartment;


    public SectionSettings()
	{

        InitializeComponent();

        db = new LocalDatabase();
        

        LocalNotificationCenter.Current.NotificationActionTapped += Current_NotificationActionTapped;
    }

    private void LoadDates()
    {
        datesContainer.Children.Clear();  // Clear any existing DatePickers

        resetCheckBoxes();

        if (compartment.Days == null || compartment.TimeAmounts == null) // if theres no dates picked beforehand, dont load anything
            return;

        for (int i = 0; i < compartment.Days.Count; i++)
        {
            int dayIndex = compartment.Days[i]; 

            if (checkBoxHolder.Children[dayIndex] is StackLayout stackLayout)
            {
                if (stackLayout.Children.Count > 0 && stackLayout.Children[0] is CheckBox checkBox)
                {
                    checkBox.IsChecked = true;
                }
            }
        }


        for (int i = 0; i < compartment.TimeAmounts.Count; i++)
        {
            var datePicker = new TimePicker
            {
                TextColor = Colors.White,
                BackgroundColor = Color.Parse("#2B5B54"),
                Time = new TimeSpan(compartment.TimeAmounts[i].hour, compartment.TimeAmounts[i].minute, 0)

            };

            datesContainer.Children.Add(datePicker);
        };
        
    }

    private void resetCheckBoxes()
    {
        for (int i = 0; i < checkBoxHolder.Children.Count; i++)
        {

            if (checkBoxHolder.Children[i] is StackLayout stackLayout)
            {
                if (stackLayout.Children.Count > 0 && stackLayout.Children[0] is CheckBox checkBox)
                {
                    checkBox.IsChecked = false;
                }
            }


        }
    }

    private void AddNewDateEntry()
    {

        var datePicker = new TimePicker
        {
            TextColor = Colors.White,
            BackgroundColor = Color.Parse("#2B5B54")
        };

        datesContainer.Children.Add(datePicker);
    }

    private void RemoveDateEntry()
    {
        IView item = datesContainer.Children[datesContainer.Children.Count-1];
        datesContainer.Children.Remove(item);
    }

    private void LoadCompartmentInfo(int value)
    {

       
        compartmentIdValue = value;
        compartment = db.GetCompartment(value);
        MedName.Text = compartment.medName;
        medAmmount.Text = compartment.amount.ToString();
        remAmmount.Text = compartment.TimeAmounts.Count.ToString();

        remindersCounter = int.Parse(remAmmount.Text);
        medsCounter = int.Parse(medAmmount.Text);

        LoadDates();


    }

    private void UpdateCompartmentInfo()
    {
        compartment.medName = MedName.Text;

        List<int> checkedIndices = new List<int>();

        for (int i = 0; i < checkBoxHolder.Children.Count; i++)
        {
            if (checkBoxHolder.Children[i] is StackLayout stackLayout)
            {
                // Assuming checkbox is always the first child (CheckBox is 0th child, Label is 1st child)
                if (stackLayout.Children[0] is CheckBox checkBox && checkBox.IsChecked)
                {
                    // Should be 1-based index from the xaml structure (Monday - 1, Tuesday - 2, etc.)
                    checkedIndices.Add(i);
                }
            }
        }

        List<(int hour, int minute)> timePickerValues = new List<(int hour, int minute)>();

        for (int i = 0; i < datesContainer.Children.Count; i++)
        {
            if (datesContainer.Children[i] is TimePicker timePicker)
            {
                
                int hour = timePicker.Time.Hours;
                int minute = timePicker.Time.Minutes;

                timePickerValues.Add((hour, minute));
            }
        }

        // Debug output
        Debug.WriteLine("Checked Indices:");
        foreach (var index in checkedIndices)
        {
            Debug.WriteLine(index);
        }

        Debug.WriteLine("Time Picker Values:");
        foreach (var timeValue in timePickerValues)
        {
            Debug.WriteLine($"{timeValue.hour}:{timeValue.minute}");
        }

        // Assuming db is your database handler
        // Update compartment information in the database using the gathered information
        db.SetCompartment(compartmentIdValue, MedName.Text, int.Parse(medAmmount.Text), int.Parse(remAmmount.Text), timePickerValues, checkedIndices);
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

        DateTime notifyDate = new DateTime();

        if (compartment.TimeAmounts.Count > 0)
            notifyDate = DateTime.Now.AddSeconds(compartment.TimeAmounts[0].hour * 3600 + compartment.TimeAmounts[0].minute * 60);
        else
            notifyDate = DateTime.Now.AddSeconds(60); // 1 min sakykim



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

    private void OnIncreaseClicked(object sender, EventArgs e)
    {
        remindersCounter++;
        remAmmount.Text = remindersCounter.ToString();
        AddNewDateEntry();
    }

    private void OnDecreaseClicked(object sender, EventArgs e)
    {
        if (remindersCounter == 0)
            return;

        remindersCounter--;
        remAmmount.Text = remindersCounter.ToString();
        RemoveDateEntry();
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