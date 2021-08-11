using PracticalShooterApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace PracticalShooterApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}