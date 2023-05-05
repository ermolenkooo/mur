using kinocat.Services;
using kinocat.Views;
using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace kinocat
{
    public partial class App : Application
    {
        static public HttpClient client;
        static public string ip = "192.168.150.218"; //phone
        //static public string ip = "192.168.0.59"; //computer
        public App()
        {
            InitializeComponent();

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };
            client = new HttpClient(httpClientHandler);
            client.DefaultRequestHeaders.Add("Accept", "application/json");


            //DependencyService.Register<MockDataStore>();
            //MainPage = new AppShell();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
