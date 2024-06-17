
using InTheHand.Net.Sockets;
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
    private BluetoothSerialControler bluetoothController;

    public SectionSettings()
	{

        InitializeComponent();

        db = new LocalDatabase();
        

        LocalNotificationCenter.Current.NotificationActionTapped += Current_NotificationActionTapped;
        bluetoothController = new BluetoothSerialControler();
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

    private async Task LoadCompartmentInfo(int value)
    {

       
        compartmentIdValue = value;
        compartment = db.GetCompartment(value);
        MedName.Text = compartment.medName;
        medAmmount.Text = compartment.amount.ToString();
        remAmmount.Text = compartment.TimeAmounts.Count.ToString();

        remindersCounter = int.Parse(remAmmount.Text);
        medsCounter = int.Parse(medAmmount.Text);

        LoadDates();

        if (BluetoothManager.BTConnectionState())
        {
            BluetoothClient bluetoothClient = await BluetoothManager.Instance.GetBluetoothClient();
            if (bluetoothClient != null && bluetoothClient.Connected)
            {
                bluetoothController.StartBreathingEffect(bluetoothClient, compartmentIdValue);
            }
        }
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

        // Get the time from each TimePicker in datesContainer
        List<DateTime> notifyDates = new List<DateTime>();
        foreach (var child in datesContainer.Children)
        {
            if (child is TimePicker timePicker)
            {
                DateTime now = DateTime.Now;
                DateTime notifyDate = new DateTime(now.Year, now.Month, now.Day, timePicker.Time.Hours, timePicker.Time.Minutes, 0);
                if (notifyDate < now)
                {
                    notifyDate = notifyDate.AddDays(1); // Schedule for the next day if time is earlier than now
                }
                notifyDates.Add(notifyDate);
            }
        }

        if (notifyDates.Count == 0)
        {
            await DisplayAlert("Klaida", "Nepasirinkta jokia laiko data", "Supratau");
            return;
        }

        int selectedNumber = int.Parse(medAmmount.Text);

        if (BluetoothManager.BTConnectionState())
        {
            BluetoothClient bluetoothClient = await BluetoothManager.Instance.GetBluetoothClient();

            if (bluetoothClient != null && bluetoothClient.Connected)
            {
                foreach (var notifyDate in notifyDates)
                {
                    bluetoothController.SendNumberAndReminderTime(bluetoothClient, selectedNumber, notifyDate);
                    bluetoothController.SendSection(bluetoothClient, compartmentIdValue);  // Send the section information
                }
                
                Console.WriteLine("Sekcija: " + compartmentIdValue);
                bluetoothController.StopBreathingEffect(bluetoothClient, compartmentIdValue);
            }
        }

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
                NotifyTime = notifyDates.First(),
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