using PracticalShooterApp.ViewModels;
using PracticalShooterApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PracticalShooterApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            
            Routing.RegisterRoute(nameof(OnboardingPage), typeof(OnboardingPage));
            Routing.RegisterRoute(nameof(LoadingPage), typeof(LoadingPage));
            Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
            Routing.RegisterRoute(nameof(RulesPage), typeof(RulesPage));
            Routing.RegisterRoute(nameof(MorePage), typeof(MorePage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
