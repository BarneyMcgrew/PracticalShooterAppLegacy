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
    public partial class RSSPage : ContentPage
    {
        private RSSPageViewModel _viewModel = new RSSPageViewModel();

        public RSSPage()
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

        private void SfChipGroup_SelectionChanged(object sender, Syncfusion.Buttons.XForms.SfChip.SelectionChangedEventArgs e)
        {
            var selectionFilterCommand = _viewModel.FilterSelectionCommand;
            selectionFilterCommand.Execute(null);
        }
    }
}