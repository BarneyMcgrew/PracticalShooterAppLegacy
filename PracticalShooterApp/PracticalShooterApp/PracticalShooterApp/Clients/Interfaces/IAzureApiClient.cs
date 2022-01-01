using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using PracticalShooterApp.Models;

namespace PracticalShooterApp.Clients.Interfaces
{
    public interface IAzureApiClient
    {
        Task<List<HomeTilesModel>> GetHomeTiles();
        Task<List<CalendarEntriesModel>> GetCalendarEntries();
    }
}