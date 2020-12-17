using FormsControls.Base;
using IPSCCompanionApp.UserInterfaces.PRACTICE;
using IPSCCompanionApp.UserInterfaces.RULEBOOKS;
using IPSCCompanionApp.UserInterfaces.UTILITIES;
using IPSCCompanionLibrary.ObjectClasses.Rules;
using IPSCCompanionLibrary.StaticResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IPSCCompanionApp.UserInterfaces.SYSTEM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenu : ContentPage, IAnimationPage
    {
        public IPageAnimation PageAnimation { get; } = new PushPageAnimation() { Duration = AnimationDuration.Long, Subtype = AnimationSubtype.FromRight };
        public MainMenu()
        {
            InitializeComponent();

            TutorialsTab.GestureRecognizers.Add(new TapGestureRecognizer((view) => OnTutorialsClicked()));
            RulebooksTab.GestureRecognizers.Add(new TapGestureRecognizer((view) => OnRulebooksClicked()));
            StageAnalysisTab.GestureRecognizers.Add(new TapGestureRecognizer((view) => OnHitFactorClicked()));
            PowerFactorTab.GestureRecognizers.Add(new TapGestureRecognizer((view) => OnPoweFactorClicked()));
            WebLinksTab.GestureRecognizers.Add(new TapGestureRecognizer((view) => OnWebLinksClicked()));
            ParTimerTab.GestureRecognizers.Add(new TapGestureRecognizer((view) => OnParTimerClicked()));
            DrillsTab.GestureRecognizers.Add(new TapGestureRecognizer((view) => OnDrillsClicked()));
            SettingsTab.GestureRecognizers.Add(new TapGestureRecognizer((view) => OnSettingsClicked()));

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (!GlobalResourceCache.Current.ResourcesLoaded)
                LoadResources();
        }

        private async Task LoadResources()
        {
            await LoadingScreenVisibility(true);

            // Load Global Settings
            await UpdateLoadingText("Loading User App Settings", "Patching those no-shoots targets you shot.");
            GlobalSettings.Current.LoadSettings(App.FolderPath);

            // Load Tutorials
            await UpdateLoadingText("Loading Tutorials...", "Kita's fitbit just hit over 9000 steps in one stage.");
            GlobalResourceCache.Current.LoadTutorials();

            // Load Rulebooks
            await UpdateLoadingText("Loading Rulebooks...", "Is this a shortcut?");
            GlobalResourceCache.Current.LoadRulebooks();

            // Load Calculators
            await UpdateLoadingText("Loading Calculators...", "Adding that coefficient you always wanted!");
            GlobalResourceCache.Current.LoadCalculators();

            // Load Utilities
            await UpdateLoadingText("Loading Utilities...", "What's the name of that IPSC website again?");
            GlobalResourceCache.Current.LoadWebsites();

            // Load Drills Library
            await UpdateLoadingText("Loading Drills...", "I'm fed up of reload exercises.");
            GlobalResourceCache.Current.LoadDrillsLibrary();

            await LoadingScreenVisibility(false);
        }

        private async Task UpdateLoadingText(string title, string description)
        {
            LoadingTitle.Text = title;
            LoadingDescription.Text = description;
            await Task.Delay(1000);
        }

        private async Task LoadingScreenVisibility(bool visible)
        {
            PopUpLayer.IsVisible = visible;
        }

        private void OnSettingsClicked()
        {
            Navigation.PushModalAsync(new SettingsPage());
        }

        private void OnDrillsClicked()
        {
        }

        private void OnParTimerClicked()
        {
        }

        private void OnWebLinksClicked()
        {
            Navigation.PushModalAsync(new WebsiteLinks());
        }

        private void OnPoweFactorClicked()
        {
        }

        private void OnHitFactorClicked()
        {
            Navigation.PushModalAsync(new HitFactorCalculator());
        }

        private void OnRulebooksClicked()
        {
            Navigation.PushModalAsync(new RulebooksPage());
        }

        private void OnTutorialsClicked()
        {
            Navigation.PushModalAsync(new TutorialsPage());
        }

        public void OnAnimationStarted(bool isPopAnimation)
        {
        }

        public void OnAnimationFinished(bool isPopAnimation)
        {
        }
    }
}