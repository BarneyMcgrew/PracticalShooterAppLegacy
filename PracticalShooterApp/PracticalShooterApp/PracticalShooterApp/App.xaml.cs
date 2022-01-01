using PracticalShooterApp.Services;
using PracticalShooterApp.Views;
using System;
using System.Globalization;
using System.Threading.Tasks;
using PracticalShooterApp.Clients;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Akavache;
using PracticalShooterApp.Helpers;

[assembly: ExportFont("Montserrat-Bold.ttf", Alias = "Montserrat-Bold")]
[assembly: ExportFont("Montserrat-Medium.ttf", Alias = "Montserrat-Medium")]
[assembly: ExportFont("Montserrat-Regular.ttf", Alias = "Montserrat-Regular")]
[assembly: ExportFont("Montserrat-SemiBold.ttf", Alias = "Montserrat-SemiBold")]
[assembly: ExportFont("UIFontIcons.ttf", Alias = "FontIcons")]
namespace PracticalShooterApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            
            VersionTracking.Track();

            var licenseKey = "NTUyMTc3QDMxMzkyZTM0MmUzMEhnOVk3Z3E1NHNwUitsaHVTbmpvWXdvdUxUeEhDLzBHdVBGRTFpNkRWelU9";
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(licenseKey);

            BlobCache.ApplicationName = "PracticalShooterApp";

            DependencyService.Register<AzureApiClient>();
            DependencyService.Register<HomeTilesService>();
            DependencyService.Register<CalendarEntriesService>();
            DependencyService.Register<BrowserService>();
            DependencyService.Register<ShareHelper>();
            DependencyService.Register<SqlRulesClient>();
            DependencyService.Register<RulebookService>();
            
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
