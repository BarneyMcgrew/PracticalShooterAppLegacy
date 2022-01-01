using System.Collections.Generic;
using System.Threading.Tasks;
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

        public RulebookService()
        {
            
        }

        public async Task<List<Rulebook>> GetAllRulebooksByLanguage(Language language)
        {
            return await _sqlClient.Table<Rulebook>().Where(o => o.Language == language).ToListAsync();
        }
    }
}