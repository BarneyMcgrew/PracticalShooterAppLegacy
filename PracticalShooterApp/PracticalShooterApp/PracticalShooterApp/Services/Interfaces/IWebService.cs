using PracticalShooterApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PracticalShooterApp.Services.Interfaces
{
    public interface IWebService
    {
        Task<RSSFeedModel> GetRSSFeed(string url);
    }
}
