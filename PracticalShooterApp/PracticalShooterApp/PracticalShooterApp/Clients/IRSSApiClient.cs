using System.Threading.Tasks;
using PracticalShooterApp.Models;

namespace PracticalShooterApp.Clients
{
    public interface IRSSApiClient
    {
        Task<RSSRawFeed> GetRSSFeed(string url);
    }
}
