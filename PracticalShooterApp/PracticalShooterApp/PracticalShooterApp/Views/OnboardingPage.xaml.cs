using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticalShooterApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OnboardingPage : ContentPage
    {
        public OnboardingPage()
        {
            InitializeComponent();

            OnboardingCarousel.CurrentItemChanged += OnCurrentItemChanged;
            ContinueButton.Clicked += ContinueButtonOnClicked;
        }

        private async void ContinueButtonOnClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//SearchPage");
        }

        private void OnCurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            ContinueButton.IsVisible = OnboardingCarousel.Position == 2;
        }
    }
}