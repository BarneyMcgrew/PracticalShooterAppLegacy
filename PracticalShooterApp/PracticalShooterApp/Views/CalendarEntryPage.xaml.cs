using PracticalShooterApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticalShooterApp.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticalShooterApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarEntryPage : ContentPage
    {
        private CalendarEntryPageViewModel _viewModel = new CalendarEntryPageViewModel();
        private IActionBarHelper _actionBarHelper => DependencyService.Get<IActionBarHelper>();
        public CalendarEntryPage()
        {
            InitializeComponent();

            BindingContext = _viewModel;
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetHeaderSafeArea();
        }

        public CalendarEntryPage(string inlineIdentifier)
        {
            InitializeComponent();

            BindingContext = _viewModel;

            _viewModel.InlineIdentifier = inlineIdentifier;
        }
        
        protected override bool OnBackButtonPressed()
        {
            var backButtonCommand = _viewModel.BackButtonCommand;
            backButtonCommand.Execute(null);
            return true;
        }
        
        private void SetHeaderSafeArea()
        {
            eventPageScroll.Margin = new Thickness(0, _actionBarHelper.GetTopSafeArea(), 0,0);
        }
    }
}