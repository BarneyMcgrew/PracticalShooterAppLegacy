using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticalShooterApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticalShooterApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RulesPage : ContentPage
    {
        private RulesPageViewModel _viewModel;

        public RulesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new RulesPageViewModel();
        }
    }
}