using PracticalShooter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticalShooter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ErrorPage : ContentPage
    {
        private ErrorViewModel _errorViewModel;

        public ErrorPage(Exception ex)
        {
            InitializeComponent();

            BindingContext = _errorViewModel = new ErrorViewModel();
            
            _errorViewModel.SetErrorMessage(ex);
        }
    }
}