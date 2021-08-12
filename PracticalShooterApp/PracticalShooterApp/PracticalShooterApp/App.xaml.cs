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

            var chapters = databaseService.GetChapters(Discipline.Handgun, Language.English);

            var sections = databaseService.GetSections(24);

            var rules = databaseService.GetRules(2);

            var glossaries = databaseService.GetGlossaries(Discipline.Handgun, Language.English);

            var appendices = databaseService.GetAppendices(Discipline.Handgun, Language.English);

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
