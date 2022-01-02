using System.Collections.Generic;
using System.Threading.Tasks;
using Akavache;
using PracticalShooterApp.Clients;
using PracticalShooterApp.Clients.Interfaces;
using PracticalShooterApp.DataModels;
using PracticalShooterApp.Enums;
using PracticalShooterApp.Services.Interfaces;
using SQLite;
using Xamarin.Forms;

namespace PracticalShooterApp.Services
{
    public class RulebookService : IRulebookService
    {
        private SQLiteAsyncConnection _sqlClient => DependencyService.Get<ISqlRulesClient>().GetConnection();
        private SettingsClient _settingsClient => DependencyService.Get<SettingsClient>();

        public RulebookService()
        { }

        public async Task<List<Rulebook>> GetAllRulebooksByCurrentLanguage()
        {
            var currentLanguage = _settingsClient.CurrentLanguage;
            
            return await GetAllRulebooksByLanguage(currentLanguage);
        }

        public async Task<Rulebook> GetCurrentRulebook()
        {
            var currentLanguage = _settingsClient.CurrentLanguage;
            var currentDiscipline = _settingsClient.CurrentDiscipline;

            return await _sqlClient.Table<Rulebook>()
                .FirstAsync(o => o.Language == currentLanguage && o.Discipline == currentDiscipline);
        }
        
        private async Task<List<Rulebook>> GetAllRulebooksByLanguage(Language language)
        {
            return await _sqlClient.Table<Rulebook>().Where(o => o.Language == language).ToListAsync();
        }
    }
}