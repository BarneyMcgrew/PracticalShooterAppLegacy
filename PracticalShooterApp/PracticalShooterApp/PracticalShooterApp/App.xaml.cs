using PracticalShooterApp.Services;
using PracticalShooterApp.Shared.Services;
using PracticalShooterApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticalShooterApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();

            var databaseService = new DatabaseService();

            databaseService.TestDatabaseConnection();

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
