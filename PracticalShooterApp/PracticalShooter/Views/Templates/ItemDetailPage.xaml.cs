using PracticalShooter.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace PracticalShooter.Views
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