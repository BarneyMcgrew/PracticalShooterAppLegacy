using PracticalShooterApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PracticalShooterApp.Clients;
using Xamarin.Forms;
using Akavache;
using System.Reactive.Linq;
using Xamarin.Essentials;

namespace PracticalShooterApp.Services
{
    public class WebService : IWebService
    {
        private IRSSApiClient _rssApiClient => DependencyService.Get<IRSSApiClient>();
        public WebService()
        { }

        public async Task<RSSFeedModel> GetRSSFeed(string url)
        {
            var rssFeed = new RSSFeedModel();

            try
            {
                rssFeed = await BlobCache.LocalMachine.GetObject<RSSFeedModel>($"rssFeed-{url}");
            }
            catch (Exception)
            {
                // No Cache Found
            }

            return rssFeed;
        }

        public async Task<RSSFeedModel> UpdateRSSFeed(string url)
        {
            RSSFeedModel rssFeed = null;

            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                var rawRSSFeed = await _rssApiClient.GetRSSFeed(url);
                rssFeed = rawRSSFeed.ConvertToModel();
            }

            if (rssFeed != null)
                await BlobCache.LocalMachine.InsertObject($"rssFeed-{url}", rssFeed, TimeSpan.FromDays(14));

            return rssFeed;
        }
    }
}
