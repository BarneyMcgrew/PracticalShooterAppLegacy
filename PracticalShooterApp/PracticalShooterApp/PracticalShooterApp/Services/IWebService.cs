using System.Threading.Tasks;
using PracticalShooterApp.Models;

namespace PracticalShooterApp.Services
{
    public interface IWebService
    {
        Task<RSSFeedModel> GetRSSFeed(string url);
    }
}
