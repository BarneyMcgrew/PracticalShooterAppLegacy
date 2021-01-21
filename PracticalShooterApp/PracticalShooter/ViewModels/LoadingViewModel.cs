using PracticalShooter.Views;
using PracticalShooterLibrary.StaticResources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PracticalShooter.ViewModels
{
    public class LoadingViewModel :BaseViewModel
    {
        private string _loadingTitle;
        private string _loadingText;

        public LoadingViewModel() 
        {
            LoadResources();
        }

        public string LoadingTitle
        {
            get => _loadingTitle;
            set => SetProperty(ref _loadingTitle, value);
        }

        public string LoadingText
        {
            get => _loadingText;
            set => SetProperty(ref _loadingText, value);
        }

        private async Task LoadResources()
        {
            // Load Global Settings
            await UpdateLoadingText("Loading User App Settings", "Patching those no-shoots targets you shot.");
            GlobalSettings.Current.LoadSettings(App.FolderPath);

            // Load Tutorials
            await UpdateLoadingText("Loading Tutorials...", "Kita's fitbit just hit over 9000 steps in one stage.");
            GlobalResourceCache.Current.LoadTutorials();

            // Load Rulebooks
            await UpdateLoadingText("Loading Rulebooks...", "Is this a shortcut?");
            GlobalResourceCache.Current.LoadRulebooks();

            // Load Rulebooks
            await UpdateLoadingText("Loading Appendices...", "Check out your Allied Equipment!");
            GlobalResourceCache.Current.LoadAppendices();

            // Load Calculators
            await UpdateLoadingText("Loading Calculators...", "Adding that coefficient you always wanted!");
            GlobalResourceCache.Current.LoadCalculators();

            // Load Utilities
            await UpdateLoadingText("Loading Utilities...", "What's the name of that IPSC website again?");
            GlobalResourceCache.Current.LoadWebsites();

            // Load Drills Library
            await UpdateLoadingText("Loading Drills...", "I'm fed up of reload exercises.");
            GlobalResourceCache.Current.LoadDrillsLibrary();

            await Shell.Current.GoToAsync("//HomePage");
        }

        private async Task UpdateLoadingText(string title, string description)
        {
            LoadingTitle = title;
            LoadingText = description;
            await Task.Delay(1000);
        }
    }
}
