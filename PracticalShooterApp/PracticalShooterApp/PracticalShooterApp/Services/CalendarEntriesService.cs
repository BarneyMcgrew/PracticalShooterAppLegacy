using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using PracticalShooterApp.Clients;
using PracticalShooterApp.Models;
using Xamarin.Forms;

namespace PracticalShooterApp.Services
{
    public class CalendarEntriesService : ICalendarEntriesService
    {
        private IAzureApiClient _apiClient => DependencyService.Get<IAzureApiClient>();
        
        public CalendarEntriesService()
        {
            
        }

        public async Task<CalendarEntriesModel> GetCalendarEntry(string inlineIdentifier)
        {
            try
            {
                var calendarEntries = await BlobCache.LocalMachine.GetObject<List<CalendarEntriesModel>>("calendarEntries");

                return calendarEntries.First(o => o.InlineIdentifier == inlineIdentifier);
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public async Task<List<CalendarEntriesModel>> GetCalendarEntries()
        {
            var calendarEntries = new List<CalendarEntriesModel>();

            try
            {
                calendarEntries = await BlobCache.LocalMachine.GetObject<List<CalendarEntriesModel>>("calendarEntries");
            }
            catch (Exception)
            {
                // No Cache Found
            }            

            return calendarEntries;
        }

        public async Task<List<CalendarEntriesModel>> UpdateCalendarEntries()
        {
            var calendarEntries = await _apiClient.GetCalendarEntries();

            if (calendarEntries.Count > 0)
                await BlobCache.LocalMachine.InsertObject("calendarEntries", calendarEntries, TimeSpan.FromDays(14));

            return calendarEntries; 
        }
    }
}