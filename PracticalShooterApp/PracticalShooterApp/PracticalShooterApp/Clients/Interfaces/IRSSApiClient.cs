using PracticalShooterApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PracticalShooterApp.Clients.Interfaces
{
    public interface IRSSApiClient
    {
        Task<RSSRawFeed> GetRSSFeed(string url);
    }
}
