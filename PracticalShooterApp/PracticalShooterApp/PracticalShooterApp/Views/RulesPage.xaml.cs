using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticalShooterApp.Shared.Interfaces.Repositories;
using PracticalShooterApp.Shared.Interfaces.Services;
using PracticalShooterApp.Shared.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticalShooterApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RulesPage : ContentPage
    {
        public RulesPage()
        {
            InitializeComponent();
        }

        private void SectionSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}