using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticalShooterApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebImageView : ContentView
    {
        public static readonly BindableProperty UrlSourceProperty = 
            BindableProperty.Create(
                nameof(ImageStream), 
                typeof(string), 
                typeof(WebImageView), 
                string.Empty);
        
        public static readonly BindableProperty UrlLinkProperty = 
            BindableProperty.Create(
                nameof(UrlLink), 
                typeof(string), 
                typeof(WebImageView), 
                string.Empty);
        
        public WebImageView()
        {
            InitializeComponent();
        }

        public string ImageStream
        {
            get => (string)GetValue(WebImageView.UrlSourceProperty);
            set => SetValue(WebImageView.UrlSourceProperty, value);
        }
        
        public string UrlLink
        {
            get => (string)GetValue(WebImageView.UrlLinkProperty);
            set => SetValue(WebImageView.UrlLinkProperty, value);
        }

        private async void OnWebImageTapped(object sender, EventArgs e)
        {
            var url = UrlLink;
            await Browser.OpenAsync(url, new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred
            });
        }
    }
}