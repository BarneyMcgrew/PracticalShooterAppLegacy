using System.Collections.Generic;
using System.Threading.Tasks;
using PracticalShooterApp.Models;

namespace PracticalShooterApp.Services.Interfaces
{
    public interface ICalendarEntriesService
    {
        Task<CalendarEntriesModel> GetCalendarEntry(string inlineIdentifier);
        Task<List<CalendarEntriesModel>> GetCalendarEntries();
        Task<List<CalendarEntriesModel>> UpdateCalendarEntries();
    }
}