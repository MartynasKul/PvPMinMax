using Plugin.LocalNotification;
using System.Diagnostics;

namespace MinMaxApp;
public partial class HomePage : ContentPage
{
    ESPController controller = new ESPController("http://192.168.4.1");
    
	public HomePage()
	{
		InitializeComponent();
        LocalNotificationCenter.Current.NotificationActionTapped += Current_NotificationActionTapped;
        Loaded += OnPageLoaded;

        
    }

    private void OnPageLoaded(object sender, EventArgs e)
    {
        LocalDatabase db = new LocalDatabase();
        Med1Button.Text = db.GetCompartment(0).ToString();
        Med2Button.Text = db.GetCompartment(1).ToString();
        Med3Button.Text = db.GetCompartment(2).ToString();
        Med4Button.Text = db.GetCompartment(3).ToString();
        Med5Button.Text = db.GetCompartment(4).ToString();
        Med6Button.Text = db.GetCompartment(5).ToString();
        Med7Button.Text = db.GetCompartment(6).ToString();
        Med8Button.Text = db.GetCompartment(7).ToString();
    }

    private void Current_NotificationActionTapped(Plugin.LocalNotification.EventArgs.NotificationActionEventArgs e)
    {
        if (e.IsDismissed)
        {
            LocalNotificationCenter.Current.CancelAll();
        }
        else if (e.IsTapped) 
        {
            LocalNotificationCenter.Current.CancelAll();
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
        await controller.DisplayNumber(1);
        Debug.WriteLine("ABOBA"+controller.DisplayNumber(1));
        
        //Notifikaciju pradzia - sukuriamas requestas, ir poto jis parodomas.
        var request = new NotificationRequest
        {
            NotificationId = 1,
            Title = "BEMEBO",
            Subtitle = Med1Button.Text ,
            Description = Med1Button.Text + " skilties atidarymas",
            BadgeNumber = 42,
            
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = DateTime.Now.AddSeconds(2),

                NotifyRepeatInterval = TimeSpan.FromSeconds(2)
                // galima dadedti kada repeatinti n shit
            }
        };
        await LocalNotificationCenter.Current.Show(request);


        //await Shell.Current.GoToAsync($"//SectionSettings?buttonId={buttonId}&originalButtonName={originalName}");
        await Shell.Current.GoToAsync($"//SectionSettings?compartment={buttonID}");
        //Navigation.PushAsync(new SectionSettings());
    }
    private async void OnMed2Clicked(object sender, EventArgs e)
    {
        int buttonID = 1;
        string originalName = Med2Button.Text;

        Med2Button.Text = originalName + " Pressed";

        await controller.DisplayNumber(2);
        //Notifikaciju pradzia - sukuriamas requestas, ir poto jis parodomas.
        var request = new NotificationRequest
        {
            NotificationId = 1,
            Title = "BEMEBO",
            Subtitle = Med2Button.Text,
            Description = Med2Button.Text + " skilties atidarymas",
            BadgeNumber = 42,
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = DateTime.Now.AddSeconds(10),

                NotifyRepeatInterval = TimeSpan.FromSeconds(10)
                // galima dadedti kada repeatinti n shit

            }
        };

        await LocalNotificationCenter.Current.Show(request);


        //await Shell.Current.GoToAsync($"//SectionSettings?buttonId={buttonId}&originalButtonName={originalName}");
        await Shell.Current.GoToAsync($"//SectionSettings?compartment={buttonID}");
        //Navigation.PushAsync(new SectionSettings());
    }
    private async void OnMed3Clicked(object sender, EventArgs e)
    {
        int buttonID = 2;
        string originalName = Med3Button.Text;

        Med3Button.Text = originalName + " Pressed";

        await controller.DisplayNumber(3);
        //Notifikaciju pradzia - sukuriamas requestas, ir poto jis parodomas.
        var request = new NotificationRequest
        {
            NotificationId = 1,
            Title = "BEMEBO",
            Subtitle = Med3Button.Text,
            Description = Med3Button.Text + " skilties atidarymas",
            BadgeNumber = 42,
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = DateTime.Now.AddSeconds(10),

                NotifyRepeatInterval = TimeSpan.FromSeconds(10)
                // galima dadedti kada repeatinti n shit

            }
        };

        await LocalNotificationCenter.Current.Show(request);
        //await Shell.Current.GoToAsync($"//SectionSettings?buttonId={buttonId}&originalButtonName={originalName}");
        await Shell.Current.GoToAsync($"//SectionSettings?compartment={buttonID}");
        //Navigation.PushAsync(new SectionSettings());
    }
    private async void OnMed4Clicked(object sender, EventArgs e)
    {
        int buttonID = 3;
        string originalName = Med4Button.Text;

        Med4Button.Text = originalName + " Pressed";

        await controller.DisplayNumber(4);
        //Notifikaciju pradzia - sukuriamas requestas, ir poto jis parodomas.
        var request = new NotificationRequest
        {
            NotificationId = 1,
            Title = "BEMEBO",
            Subtitle = Med4Button.Text,
            Description = Med4Button.Text + " skilties atidarymas",
            BadgeNumber = 42,
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = DateTime.Now.AddSeconds(10),

                NotifyRepeatInterval = TimeSpan.FromSeconds(10)
                // galima dadedti kada repeatinti n shit

            }
        };

        await LocalNotificationCenter.Current.Show(request);
        //await Shell.Current.GoToAsync($"//SectionSettings?buttonId={buttonId}&originalButtonName={originalName}");
        await Shell.Current.GoToAsync($"//SectionSettings?compartment={buttonID}");
        //Navigation.PushAsync(new SectionSettings());
    }
    private async void OnMed5Clicked(object sender, EventArgs e)
    {
        int buttonID = 4;
        string originalName = Med5Button.Text;

        Med5Button.Text = originalName + " Pressed";

        await controller.DisplayNumber(5);
        //Notifikaciju pradzia - sukuriamas requestas, ir poto jis parodomas.
        var request = new NotificationRequest
        {
            NotificationId = 1,
            Title = "BEMEBO",
            Subtitle = Med5Button.Text,
            Description = Med5Button.Text + " skilties atidarymas",
            BadgeNumber = 42,
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = DateTime.Now.AddSeconds(10),

                NotifyRepeatInterval = TimeSpan.FromSeconds(10)
                // galima dadedti kada repeatinti n shit

            }
        };

        await LocalNotificationCenter.Current.Show(request);

        //await Shell.Current.GoToAsync($"//SectionSettings?buttonId={buttonId}&originalButtonName={originalName}");
        await Shell.Current.GoToAsync($"//SectionSettings?compartment={buttonID}");
        //Navigation.PushAsync(new SectionSettings());
    }
    private async void OnMed6Clicked(object sender, EventArgs e)
    {
        int buttonID = 5;
        string originalName = Med6Button.Text;

        Med6Button.Text = originalName + " Pressed";

        await controller.DisplayNumber(6);
        //Notifikaciju pradzia - sukuriamas requestas, ir poto jis parodomas.
        var request = new NotificationRequest
        {
            NotificationId = 1,
            Title = "BEMEBO",
            Subtitle = Med6Button.Text,
            Description = Med6Button.Text + " skilties atidarymas",
            BadgeNumber = 42,
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = DateTime.Now.AddSeconds(10),

                NotifyRepeatInterval = TimeSpan.FromSeconds(10)
                // galima dadedti kada repeatinti n shit

            }
        };

        await LocalNotificationCenter.Current.Show(request);

        //await Shell.Current.GoToAsync($"//SectionSettings?buttonId={buttonId}&originalButtonName={originalName}");
        await Shell.Current.GoToAsync($"//SectionSettings?compartment={buttonID}");
        //Navigation.PushAsync(new SectionSettings());
    }
    private async void OnMed7Clicked(object sender, EventArgs e)
    {
        int buttonID = 6;
        string originalName = Med7Button.Text;

        Med7Button.Text = originalName + " Pressed";

        await controller.DisplayNumber(7);
        //Notifikaciju pradzia - sukuriamas requestas, ir poto jis parodomas.
        var request = new NotificationRequest
        {
            NotificationId = 1,
            Title = "BEMEBO",
            Subtitle = Med7Button.Text,
            Description = Med7Button.Text + " skilties atidarymas",
            BadgeNumber = 42,
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = DateTime.Now.AddSeconds(10),

                NotifyRepeatInterval = TimeSpan.FromSeconds(10)
                // galima dadedti kada repeatinti n shit

            }
        };

        await LocalNotificationCenter.Current.Show(request);

        //await Shell.Current.GoToAsync($"//SectionSettings?buttonId={buttonId}&originalButtonName={originalName}");
        await Shell.Current.GoToAsync($"//SectionSettings?compartment={buttonID}");
        //Navigation.PushAsync(new SectionSettings());
    }
    private async void OnMed8Clicked(object sender, EventArgs e)
    {
        int buttonID = 7;
        string originalName = Med8Button.Text;

        Med8Button.Text = originalName + " Pressed";

        await controller.DisplayNumber(8);
        //Notifikaciju pradzia - sukuriamas requestas, ir poto jis parodomas.
        var request = new NotificationRequest
        {
            NotificationId = 1,
            Title = "BEMEBO",
            Subtitle = Med8Button.Text,
            Description = Med8Button.Text + " skilties atidarymas",
            BadgeNumber = 42,
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = DateTime.Now.AddSeconds(10),

                NotifyRepeatInterval = TimeSpan.FromSeconds(10)
                // galima dadedti kada repeatinti n shit

            }
        };

        await LocalNotificationCenter.Current.Show(request);

        //await Shell.Current.GoToAsync($"//SectionSettings?buttonId={buttonId}&originalButtonName={originalName}");
        await Shell.Current.GoToAsync($"//SectionSettings?compartment={buttonID}");
        //Navigation.PushAsync(new SectionSettings());
    }



}