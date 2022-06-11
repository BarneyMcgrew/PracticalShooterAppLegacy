using System;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace PracticalShooterApp.ViewModels
{
    [QueryProperty(nameof(InlineIdentifier), nameof(InlineIdentifier))]
    [QueryProperty(nameof(Title), nameof(Title))]
    [QueryProperty(nameof(SearchCriteria), nameof(SearchCriteria))]
    [QueryProperty(nameof(ShowControls), nameof(ShowControls))]
    public class PDFPageViewModel : BaseViewModel
    {
        private string _title;
        private string _showControls;
        private bool _showControlsBoolean;
        private string _searchCriteria;
        private string _inlineIdentifier;
        private Stream _pdfStream;

        private Command<object> backButtonCommand;
        
        public PDFPageViewModel()
        {}
        
        public Command<object> BackButtonCommand => this.backButtonCommand ?? 
                                                    (this.backButtonCommand = new Command<object>(this.BackButtonClicked));

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string ShowControls
        {
            get => _showControls;
            set
            {
                SetProperty(ref _showControls, value);
                if (value.ToLowerInvariant() == "true")
                    ShowControlsBoolean = true;
            }
        }

        public bool ShowControlsBoolean
        {
            get => _showControlsBoolean;
            set => SetProperty(ref _showControlsBoolean, value);
        }
        
        public string SearchCriteria
        {
            get => _searchCriteria;
            set => SetProperty(ref _searchCriteria, value);
        }
        
        public string InlineIdentifier
        {
            get => _inlineIdentifier;
            set
            {
                SetProperty(ref _inlineIdentifier, value);
                GetPDFStream();
            }
        }

        public Stream PDFStream
        {
            get => _pdfStream;
            set => SetProperty(ref _pdfStream, value);
        }

        public void GetPDFStream()
        {
            IsBusy = true;
            
            PDFStream = typeof(App).GetTypeInfo().Assembly.GetManifestResourceStream(InlineIdentifier);

            if (ShowControls == "true")
                ShowControlsBoolean = true;
            
            IsBusy = false;
        }
        
        private async void BackButtonClicked(object obj)
        {
            await Shell.Current.GoToAsync("..", true);
        }
    }
}