using PracticalShooterApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PracticalShooterApp.Clients;
using Xamarin.Forms;

namespace PracticalShooterApp.Services
{
    public class WebService : IWebService
    {
        private IRSSApiClient _rssApiClient => DependencyService.Get<IRSSApiClient>();
        public WebService()
        { }

        public async Task<RSSFeedModel> GetRSSFeed(string url)
        {
            var rawRSSFeed = await _rssApiClient.GetRSSFeed(url);

            return rawRSSFeed.ConvertToModel();
        }
    }
}
