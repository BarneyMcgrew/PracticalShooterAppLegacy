using PracticalShooterApp.Services;
using PracticalShooterApp.Shared.Services;
using PracticalShooterApp.Views;
using System;
using PracticalShooterApp.Shared.Enums;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticalShooterApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            
            VersionTracking.Track();
            Akavache.Registrations.Start("ThePracticalShooterApp");

            //DependencyService.Register<MockDataStore>();

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
