﻿using PracticalShooterApp.Services;
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

            var licenseKey = "NjQ2OTE5QDMyMzAyZTMxMmUzMERLakRtQjdrOUNhcDJ5QWg2eksyNUxYdERaQ0JiQ2tQcnNzcjl3QWRMT009";
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(licenseKey);

            BlobCache.ApplicationName = "PracticalShooterApp";

            
            DependencyService.Register<HomeTilesService>();
            DependencyService.Register<CalendarEntriesService>();
            DependencyService.Register<BrowserService>();
            DependencyService.Register<RulebookService>();
            DependencyService.Register<ChapterService>();
            DependencyService.Register<SectionService>();
            DependencyService.Register<AppendixService>();
            DependencyService.Register<RulesService>();
            DependencyService.Register<WebService>();
            DependencyService.Register<SearchService>();
            DependencyService.Register<GlossaryService>();
            
            DependencyService.Register<ShareHelper>();
            DependencyService.Register<RSSItemHelper>();
            DependencyService.Register<ActionBarHelper>();
            
            DependencyService.Register<AzureApiClient>();
            DependencyService.Register<RSSApiClient>();
            DependencyService.Register<SqlRulesClient>();
            DependencyService.Register<SettingsClient>();
            
            
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
