using System;
using System.Threading.Tasks;
using PracticalShooterApp.Shared.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PracticalShooterApp.ViewModels
{
    public class LoadingViewModel : BaseViewModel
    {
        private string _urlSource;
        private string _urlLink;
        private string _loadingText;
        private double _loadingValue;

        public LoadingViewModel()
        {
            UrlLink = "https://tactree.co.uk";
            UrlSource = "https://images.milledcdn.com/2021-01-27/FqPwGqEBi1kQXMC6/QtrVZeIt-Wq5.jpeg";

            LoadResources();
        }

        public string UrlSource
        {
            get => _urlSource;
            set => SetProperty(ref _urlSource, value);
        }

        public string UrlLink
        {
            get => _urlLink;
            set => SetProperty(ref _urlLink, value);
        }

        public string LoadingText
        {
            get => _loadingText;
            set => SetProperty(ref _loadingText, value);
        }

        public double LoadingValue
        {
            get => _loadingValue;
            set => SetProperty(ref _loadingValue, value);
        }

        private async Task LoadResources()
        {
            // Load Fake Resources
            await UpdateLoadingText("Loading Resources...");

            var settingsService = new SettingsService();

            if (VersionTracking.IsFirstLaunchForCurrentBuild ||
                settingsService.ShowOnboarding)
            {
                await Shell.Current.GoToAsync("//OnboardingPage");
            }
            else
            {
                await Shell.Current.GoToAsync("//SearchPage");
            }
        }

        private async Task UpdateLoadingText(string text)
        {
            _loadingText = text;
            
            for (int i = 0; i < 10; i++) {
                LoadingValue = Convert.ToDouble("00." + i.ToString());  
                await Task.Delay(500);  
            }

            LoadingValue = 1;
        }
    }
}