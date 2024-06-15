using InTheHand.Net.Sockets;
using Plugin.LocalNotification;
using System.Diagnostics;

namespace MinMaxApp;
public partial class HomePage : ContentPage
{

    //Bluetooth
    private BluetoothSerialControler bluetoothController;
    private BluetoothClient bluetoothClient;
    public HomePage()
	{
		InitializeComponent();
        LocalNotificationCenter.Current.NotificationActionTapped += Current_NotificationActionTapped;
        Loaded += OnPageLoaded;

        //Bluetooth
        bluetoothController = new BluetoothSerialControler();
    }



    private void OnPageLoaded(object sender, EventArgs e)
    {
        LocalDatabase db = new LocalDatabase();
        
        for(int i = 1; i <= 8; i++)
        {
            string btnName = $"Med{i}Button";
            Button medButton = this.FindByName<Button>(btnName);
            if(medButton == null)
            {
                break;
            }
            medButton.Text = db.GetCompartment(i-1).ToString();
            if (medButton.Text.Equals("+"))
                medButton.FontSize = 60;
            else
                medButton.FontSize = 18;
        }

        

    }

    private void Current_NotificationActionTapped(Plugin.LocalNotification.EventArgs.NotificationActionEventArgs e)
    {
        if (e.IsDismissed)
        {
            LocalNotificationCenter.Current.Cancel();
        }
        else if (e.IsTapped) 
        {
            LocalNotificationCenter.Current.Cancel();
        }
        
        throw new NotImplementedException();
    }

    private void OpenSettings(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SettingsPage()); // pereina i homescreen
    }

    private async void OnMed1Clicked(object sender, EventArgs e)
    {
        int buttonID = 0;
        string originalName = Med1Button.Text;

        Med1Button.Text = originalName + " Pressed";

        //Bluetooth
        // Retrieve the shared BluetoothClient instance

        if(BluetoothManager.BTConnectionState())
        {
            BluetoothClient bluetoothClient = await BluetoothManager.Instance.GetBluetoothClient();

            if (bluetoothClient != null && bluetoothClient.Connected)
            {
                // Send the number 1 to the ESP32
                bluetoothController.SendNumber(bluetoothClient, 1);
            }
        }
        


        //await Shell.Current.GoToAsync($"//SectionSettings?buttonId={buttonId}&originalButtonName={originalName}");
        await Shell.Current.GoToAsync($"//SectionSettings?compartment={buttonID}");
        //Navigation.PushAsync(new SectionSettings());
    }
    private async void OnMed2Clicked(object sender, EventArgs e)
    {
        int buttonID = 1;
        string originalName = Med2Button.Text;

        Med2Button.Text = originalName + " Pressed";

        // Retrieve the shared BluetoothClient instance
        if (BluetoothManager.BTConnectionState())
        {
            BluetoothClient bluetoothClient = await BluetoothManager.Instance.GetBluetoothClient();

            if (bluetoothClient != null && bluetoothClient.Connected)
            {
                // Send the number 1 to the ESP32
                bluetoothController.SendNumber(bluetoothClient, 2);
            }
        }
        


        //await Shell.Current.GoToAsync($"//SectionSettings?buttonId={buttonId}&originalButtonName={originalName}");
        await Shell.Current.GoToAsync($"//SectionSettings?compartment={buttonID}");
        //Navigation.PushAsync(new SectionSettings());
    }
    private async void OnMed3Clicked(object sender, EventArgs e)
    {
        int buttonID = 2;
        string originalName = Med3Button.Text;

        Med3Button.Text = originalName + " Pressed";

        // Retrieve the shared BluetoothClient instance
        if (BluetoothManager.BTConnectionState())
        {
            BluetoothClient bluetoothClient = await BluetoothManager.Instance.GetBluetoothClient();

            if (bluetoothClient != null && bluetoothClient.Connected)
            {
                // Send the number 1 to the ESP32
                bluetoothController.SendNumber(bluetoothClient, 3);
            }
        }

        //await Shell.Current.GoToAsync($"//SectionSettings?buttonId={buttonId}&originalButtonName={originalName}");
        await Shell.Current.GoToAsync($"//SectionSettings?compartment={buttonID}");
        //Navigation.PushAsync(new SectionSettings());
    }
    private async void OnMed4Clicked(object sender, EventArgs e)
    {
        int buttonID = 3;
        string originalName = Med4Button.Text;

        Med4Button.Text = originalName + " Pressed";

        // Retrieve the shared BluetoothClient instance
        if (BluetoothManager.BTConnectionState())
        {
            BluetoothClient bluetoothClient = await BluetoothManager.Instance.GetBluetoothClient();

            if (bluetoothClient != null && bluetoothClient.Connected)
            {
                // Send the number 1 to the ESP32
                bluetoothController.SendNumber(bluetoothClient, 4);
            }
        }

        //Notifikaciju pradzia - sukuriamas requestas, ir poto jis parodomas.

        //await Shell.Current.GoToAsync($"//SectionSettings?buttonId={buttonId}&originalButtonName={originalName}");
        await Shell.Current.GoToAsync($"//SectionSettings?compartment={buttonID}");
        //Navigation.PushAsync(new SectionSettings());
    }
    private async void OnMed5Clicked(object sender, EventArgs e)
    {
        int buttonID = 4;
        string originalName = Med5Button.Text;

        Med5Button.Text = originalName + " Pressed";

        // Retrieve the shared BluetoothClient instance
        if (BluetoothManager.BTConnectionState())
        {
            BluetoothClient bluetoothClient = await BluetoothManager.Instance.GetBluetoothClient();

            if (bluetoothClient != null && bluetoothClient.Connected)
            {
                // Send the number 1 to the ESP32
                bluetoothController.SendNumber(bluetoothClient, 5);
            }
        }
        //Notifikaciju pradzia - sukuriamas requestas, ir poto jis parodomas.


        //await Shell.Current.GoToAsync($"//SectionSettings?buttonId={buttonId}&originalButtonName={originalName}");
        await Shell.Current.GoToAsync($"//SectionSettings?compartment={buttonID}");
        //Navigation.PushAsync(new SectionSettings());
    }
    private async void OnMed6Clicked(object sender, EventArgs e)
    {
        int buttonID = 5;
        string originalName = Med6Button.Text;

        Med6Button.Text = originalName + " Pressed";

        // Retrieve the shared BluetoothClient instance
        if (BluetoothManager.BTConnectionState())
        {
            BluetoothClient bluetoothClient = await BluetoothManager.Instance.GetBluetoothClient();

            if (bluetoothClient != null && bluetoothClient.Connected)
            {
                // Send the number 1 to the ESP32
                bluetoothController.SendNumber(bluetoothClient, 6);
            }
        }

        //Notifikaciju pradzia - sukuriamas requestas, ir poto jis parodomas.


        //await Shell.Current.GoToAsync($"//SectionSettings?buttonId={buttonId}&originalButtonName={originalName}");
        await Shell.Current.GoToAsync($"//SectionSettings?compartment={buttonID}");
        //Navigation.PushAsync(new SectionSettings());
    }
    private async void OnMed7Clicked(object sender, EventArgs e)
    {
        int buttonID = 6;
        string originalName = Med7Button.Text;

        Med7Button.Text = originalName + " Pressed";

        // Retrieve the shared BluetoothClient instance
        if (BluetoothManager.BTConnectionState())
        {
            BluetoothClient bluetoothClient = await BluetoothManager.Instance.GetBluetoothClient();

            if (bluetoothClient != null && bluetoothClient.Connected)
            {
                // Send the number 1 to the ESP32
                bluetoothController.SendNumber(bluetoothClient, 7);
            }
        }

        //Notifikaciju pradzia - sukuriamas requestas, ir poto jis parodomas.

        //await Shell.Current.GoToAsync($"//SectionSettings?buttonId={buttonId}&originalButtonName={originalName}");
        await Shell.Current.GoToAsync($"//SectionSettings?compartment={buttonID}");
        //Navigation.PushAsync(new SectionSettings());
    }
    private async void OnMed8Clicked(object sender, EventArgs e)
    {
        int buttonID = 7;
        string originalName = Med8Button.Text;

        // Retrieve the shared BluetoothClient instance
        if (BluetoothManager.BTConnectionState())
        {
            BluetoothClient bluetoothClient = await BluetoothManager.Instance.GetBluetoothClient();

            if (bluetoothClient != null && bluetoothClient.Connected)
            {
                // Send the number 1 to the ESP32
                bluetoothController.SendNumber(bluetoothClient, 8);
            }
        }

        Med8Button.Text = originalName + " Pressed";

        //Notifikaciju pradzia - sukuriamas requestas, ir poto jis parodomas.
        
        //await Shell.Current.GoToAsync($"//SectionSettings?buttonId={buttonId}&originalButtonName={originalName}");
        await Shell.Current.GoToAsync($"//SectionSettings?compartment={buttonID}");
        //Navigation.PushAsync(new SectionSettings());
    }



}