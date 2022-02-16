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
        private readonly RSSPageViewModel _viewModel = new RSSPageViewModel();

        public RSSPage()
        {
            InitializeComponent();

            BindingContext = _viewModel;
        }
    }
}