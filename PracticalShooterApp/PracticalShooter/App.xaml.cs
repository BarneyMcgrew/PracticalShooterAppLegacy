using PracticalShooter.Services;
using PracticalShooter.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticalShooter
{
    public partial class App : Application
    {
        public static string FolderPath;

        public App(Exception ex)
        {
            InitializeComponent();

            MainPage = new ErrorPage(ex);
        }

        public App()
        {
            InitializeComponent();

            FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

            DependencyService.Register<MockDataStore>();

            MainPage = new AppShell();
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
