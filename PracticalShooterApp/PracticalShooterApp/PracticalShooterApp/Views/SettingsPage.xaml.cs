using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticalShooterApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void OnDeveloperTapped(object sender, EventArgs e)
        {
            const string url = "https://www.themockachino.com";
            Task.Run(async () => await Browser.OpenAsync(url, new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred
            }));
        }
        
        private void OnCreatorTapped(object sender, EventArgs e)
        {
            const string url = "https://www.barney-mcgrew.com";
            Task.Run(async () => await Browser.OpenAsync(url, new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred
            }));
        }
    }
}