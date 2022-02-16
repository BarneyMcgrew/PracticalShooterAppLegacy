using PracticalShooterApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticalShooterApp.Services;
using Xamarin.Forms;

namespace PracticalShooterApp.ViewModels
{
    [QueryProperty(nameof(ContextFeed), nameof(ContextFeed))]
    public class RSSPageViewModel : BaseViewModel
    {
        private string _contextFeed;

        private IWebService _webService => DependencyService.Get<IWebService>();

        public RSSPageViewModel()
        {
            Title = "RSS Feed";
        }

        public string ContextFeed
        {
            get => _contextFeed;
            set
            {
                _contextFeed = value;
                LoadRSSFeed(value);
            }
        }

        private async Task<RSSFeedModel> LoadRSSFeed(string rssFeed)
        {
            try
            {
                var model = await _webService.GetRSSFeed(rssFeed);

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
