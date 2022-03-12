using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticalShooterApp.Helpers;
using PracticalShooterApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticalShooterApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MorePage : ContentPage
    {
        private IActionBarHelper _actionBarHelper => DependencyService.Get<IActionBarHelper>();
        private IBrowserService _browserService => DependencyService.Get<IBrowserService>();

        public MorePage()
        {
            InitializeComponent();
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetHeaderSafeArea();
        }
        
        private void SetHeaderSafeArea()
        {
            mainGrid.Margin = new Thickness(0, _actionBarHelper.GetTopSafeArea(), 0,0);
        }

        private void settingsButton_Clicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private async void eventsCalendar_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//CalendarPage");
        }

        private void hitFactorCalculator_Clicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void trainingParTimer_Clicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void stageBriefCreator_Clicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void devByBarneyMcGrew_Clicked(object sender, EventArgs e)
        {
            var uri = new Uri("https://www.barney-mcgrew.com");
            _browserService.GoToLink(uri);
        }

        private void developmentBlog_Clicked(object sender, EventArgs e)
        {
            var uri = new Uri("https://www.barney-mcgrew.com/category/the-practical-shooter-app/");
            _browserService.GoToLink(uri);
        }

        private void psaFacebook_Clicked(object sender, EventArgs e)
        {
            var uri = new Uri("https://www.facebook.com/practicalshooterapp");
            _browserService.GoToLink(uri);
        }

        private void bmgFacebook_Clicked(object sender, EventArgs e)
        {
            var uri = new Uri("https://www.facebook.com/barneymcgrewltd");
            _browserService.GoToLink(uri);
        }

        private void joinMailingList_Clicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void rateApp_Clicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void reportABug_Clicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}