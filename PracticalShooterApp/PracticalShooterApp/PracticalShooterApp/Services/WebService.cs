using PracticalShooterApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PracticalShooterApp.Clients;
using Xamarin.Forms;
using Akavache;
using System.Reactive.Linq;

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
            var rawRSSFeed = await _rssApiClient.GetRSSFeed(url);
            var rssFeed = rawRSSFeed.ConvertToModel();

            if (rssFeed != null)
                await BlobCache.LocalMachine.InsertObject($"rssFeed-{url}", rssFeed, TimeSpan.FromDays(14));

            return rssFeed;
        }
    }
}
