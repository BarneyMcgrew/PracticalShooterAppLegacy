using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticalShooterApp.Helpers;
using PracticalShooterApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticalShooterApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebPage : ContentPage
    {
        private IActionBarHelper _actionBarHelper => DependencyService.Get<IActionBarHelper>();
        
        private WebPageViewModel _viewModel = new WebPageViewModel();

        public WebPage()
        {
            InitializeComponent();

            BindingContext = _viewModel;
        }
        
        protected override bool OnBackButtonPressed()
        {
            var backButtonCommand = _viewModel.BackButtonCommand;
            backButtonCommand.Execute(null);
            return true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetHeaderSafeArea();

            _viewModel.IsBusy = true;
            
            webView.Source = _viewModel.Url;

            _viewModel.IsBusy = false;
        }
        
        private void SetHeaderSafeArea()
        {
            webPageView.Margin = new Thickness(0, _actionBarHelper.GetTopSafeArea(), 0,0);
        }
    }
}