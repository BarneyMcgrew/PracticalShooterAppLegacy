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
    public partial class SectionPage : ContentPage
    {
        public SectionViewModel _viewModel;

        public SectionPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new SectionViewModel();
        }
    }
}