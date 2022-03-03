using PracticalShooterApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.XForms.Buttons;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticalShooterApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private readonly HomePageViewModel _viewModel = new HomePageViewModel();
        public HomePage()
        {
            InitializeComponent();

            this.BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _viewModel.PopulateHomeTiles();
        }

        private void settingsButton_Clicked(object sender, EventArgs e)
        {
            popupSettings.Show();            
        }

        private void ToggleButton_OnStateChanged(object sender, StateChangedEventArgs e)
        {
            _viewModel.FilterCheckedCommand.Execute(sender);
        }
    }
}