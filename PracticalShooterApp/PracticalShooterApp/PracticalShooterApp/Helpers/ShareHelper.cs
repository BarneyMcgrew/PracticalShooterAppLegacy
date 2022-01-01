using System.Threading.Tasks;
using PracticalShooterApp.Helpers.Interfaces;
using Xamarin.Essentials;

namespace PracticalShooterApp.Helpers
{
    public class ShareHelper : IShareHelper
    {
        public async Task ShareText(string title, string text)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = title
            });
        }

        public async Task ShareUri(string title, string uri)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = uri,
                Title = title
            });
        }
    }
}