using System.Diagnostics;

namespace MauiAppEmbedIO
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            webView.Source = $"http://127.0.0.1:1234/index.htm";

            webView.Loaded += WebView_Loaded;
        }

        private void WebView_Loaded(object? sender, EventArgs e)
        {
            ApplicationController.UpdateProgress(50);
        }

        protected override async void OnAppearing()
        {
            
            //await webView.EvaluateJavaScriptAsync($"chamadaInicialDoCSharp()");
        }


        public void Test(String message)
        {
            Debug.WriteLine(message);
        }
    }

}
