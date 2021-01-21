using PracticalShooter.ViewModels;
using PracticalShooterLibrary.StaticResources;
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
    public partial class PDFPage : ContentPage
    {
        public PDFViewModel _viewModel;

        public PDFPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new PDFViewModel();

            HeaderTab.Text = RulebookSession.Current.CurrentPDFFriendlyName;
            var fileName = RulebookSession.Current.CurrentPDFFileName;

            var pdfName = $"{fileName}.pdf";
            PDFView.Uri = pdfName;

            HeaderTab.GestureRecognizers.Add(new TapGestureRecognizer((view) => GoBackClicked()));
        }

        private void GoBackClicked()
        {
            AppShell.Current.Navigation.PopAsync();
        }
    }
}