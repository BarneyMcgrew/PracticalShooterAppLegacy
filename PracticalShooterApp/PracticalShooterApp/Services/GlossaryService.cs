using System.Collections.Generic;
using System.Threading.Tasks;
using PracticalShooterApp.Clients;
using PracticalShooterApp.DataModels;
using SQLite;
using Xamarin.Forms;

namespace PracticalShooterApp.Services
{
    public class GlossaryService : IGlossaryService
    {
        private SQLiteAsyncConnection _sqlClient => DependencyService.Get<ISqlRulesClient>().GetConnection();
        private IRulebookService _rulebookService => DependencyService.Get<IRulebookService>();
        
        public GlossaryService()
        {}

        public async Task<List<Glossary>> GetAllGlossaries()
        {
            return await _sqlClient.Table<Glossary>().ToListAsync();
        }

        public async Task<List<Glossary>> GetGlossariesFromCurrentRulebook()
        {
            var rulebook = await _rulebookService.GetCurrentRulebook();

            return await _sqlClient.Table<Glossary>().Where(o => o.RulebookId == rulebook.Id).ToListAsync();
        }
    }
}