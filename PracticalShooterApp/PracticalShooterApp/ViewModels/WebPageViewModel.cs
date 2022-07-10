using System;
using SQLitePCL;
using Xamarin.Forms;

namespace PracticalShooterApp.ViewModels
{
    [QueryProperty(nameof(Title), nameof(Title))]
    [QueryProperty(nameof(Url), nameof(Url))]
    public class WebPageViewModel : BaseViewModel
    {
        private Command<object> backButtonCommand;
        private string _title;
        private string _url;
        private Uri _uri;
        
        public WebPageViewModel()
        {}
        
        public Command<object> BackButtonCommand => this.backButtonCommand ?? 
                                                    (this.backButtonCommand = new Command<object>(this.BackButtonClicked));
        
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                SetProperty(ref _title, value);
            }
        }
        
        public string Url
        {
            get => _url;
            set
            {
                _url = value;
                SetUriStream();
            }
        }

        public Uri Uri
        {
            get => _uri;
            set
            {
                _uri = value;
                SetProperty(ref _uri, value);
            }
        }

        private void SetUriStream()
        {
            IsBusy = true;

            Uri = new Uri(Url);
            
            IsBusy = false;
        }

        private async void BackButtonClicked(object obj)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}