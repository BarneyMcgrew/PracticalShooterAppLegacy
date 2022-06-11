using System.Collections.Generic;
using System.Threading.Tasks;
using PracticalShooterApp.Models;

namespace PracticalShooterApp.Clients
{
    public interface IAzureApiClient
    {
        Task<List<HomeTilesModel>> GetHomeTiles();
        Task<List<CalendarEntriesModel>> GetCalendarEntries();
    }
}