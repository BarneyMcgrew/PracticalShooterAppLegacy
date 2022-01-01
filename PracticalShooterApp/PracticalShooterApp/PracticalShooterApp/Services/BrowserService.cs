using System;
using PracticalShooterApp.Services.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PracticalShooterApp.Services
{
    public class BrowserService : IBrowserService
    {
        public BrowserService()
        {
            
        }

        public async void GoToLink(Uri uri)
        {
            try
            {
                await Browser.OpenAsync(uri, 
                    new BrowserLaunchOptions()
                    {
                        LaunchMode = BrowserLaunchMode.SystemPreferred,
                        TitleMode = BrowserTitleMode.Show,
                        PreferredToolbarColor = (Color)Application.Current.Resources["PrimaryDark"],
                        PreferredControlColor = (Color)Application.Current.Resources["Tertiary"]
                    });
            }
            catch(Exception ex)
            {
                // An unexpected error occured. No browser may be installed on the device.
            }
        }
    }
}