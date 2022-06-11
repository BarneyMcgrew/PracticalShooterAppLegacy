using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticalShooterApp.Clients;
using PracticalShooterApp.Helpers;
using PracticalShooterApp.ViewModels;
using Syncfusion.SfPdfViewer.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticalShooterApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PDFPage : ContentPage
    {
        private IActionBarHelper _actionBarHelper => DependencyService.Get<IActionBarHelper>();
        private PDFPageViewModel _viewModel = new PDFPageViewModel();
        
        public PDFPage()
        {
            InitializeComponent();
            
            BindingContext = _viewModel;
            
            pdfViewerControl.DocumentLoaded += PdfViewerControlOnDocumentLoaded;
        }

        private void PdfViewerControlOnDocumentLoaded(object sender, EventArgs args)
        {
            if (!string.IsNullOrWhiteSpace(_viewModel.SearchCriteria))
                searchButton.SendClicked();
        }

        protected override bool OnBackButtonPressed()
        {
            var backButtonCommand = _viewModel.BackButtonCommand;
            backButtonCommand.Execute(null);
            return true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetHeaderSafeArea();
        }

        private void SetHeaderSafeArea()
        {
            popupPDF.Margin = new Thickness(0, _actionBarHelper.GetTopSafeArea(), 0,0);
        }
    }
}