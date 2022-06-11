using PracticalShooterApp.ViewModels;
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
    public partial class CalendarEntryPage : ContentPage
    {
        private CalendarEntryPageViewModel _viewModel = new CalendarEntryPageViewModel();
        public CalendarEntryPage()
        {
            InitializeComponent();

            BindingContext = _viewModel;
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
    }
}