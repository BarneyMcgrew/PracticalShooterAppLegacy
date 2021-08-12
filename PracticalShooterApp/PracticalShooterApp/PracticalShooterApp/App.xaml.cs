using PracticalShooterApp.Services;
using PracticalShooterApp.Shared.Services;
using PracticalShooterApp.Views;
using System;
using PracticalShooterApp.Shared.Enums;
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

            var rulesResults = databaseService.SearchRules("barrier", Discipline.Handgun, Language.English);

            var appendixResults = databaseService.SearchAppendices("allied", Discipline.Handgun, Language.English);

            var glossaryResults = databaseService.SearchGlossaries("aim", Discipline.Handgun, Language.English);
            
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
