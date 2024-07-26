namespace MauiAppEmbedIO
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var appWebServer = new AppWebServer();
            appWebServer.CreateWebServer();

            MainPage = new AppShell();
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

            window.Width = 600;
            window.Height = 350;

            return window;
        }
    }
}
