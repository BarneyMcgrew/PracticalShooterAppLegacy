using System.Collections.Generic;
using System.Threading.Tasks;
using PracticalShooterApp.Models;

namespace PracticalShooterApp.Clients
{
    public interface IAzureApiClient
    {
        Task<List<HomeTilesModel>> GetHomeTiles();
        Task<List<InAppMessageModel>> GetInAppMessages();
        Task<List<CalendarEntriesModel>> GetCalendarEntries();
    }
}