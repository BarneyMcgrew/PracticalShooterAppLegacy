using PracticalShooterApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticalShooterApp.Helpers;
using Syncfusion.XForms.Buttons;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticalShooterApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private IActionBarHelper _actionBarHelper => DependencyService.Get<IActionBarHelper>();
        
        private readonly HomePageViewModel _viewModel = new HomePageViewModel();
        public HomePage()
        {
            InitializeComponent();

            this.BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetHeaderSafeArea();

            _viewModel.PopulateHomeTiles();
        }

        private void SetHeaderSafeArea()
        {
            popUpHome.Margin = new Thickness(0, _actionBarHelper.GetTopSafeArea(), 0,0);
        }

        private void filterButton_Clicked(object sender, EventArgs e)
        {
            popUpHome.Show();            
        }

        private void ToggleButton_OnStateChanged(object sender, StateChangedEventArgs e)
        {
            _viewModel.FilterCheckedCommand.Execute(sender);
        }
    }
}