namespace MinMaxApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // What does this do idk yet
            Routing.RegisterRoute(nameof(ChooseConnectionPage), typeof(ChooseConnectionPage));
        }
    }
}
