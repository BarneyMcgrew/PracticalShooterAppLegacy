using System.Collections.Generic;
using System.Threading.Tasks;
using PracticalShooterApp.Clients;
using PracticalShooterApp.DataModels;
using SQLite;
using Xamarin.Forms;

namespace PracticalShooterApp.Services
{
    public class ChapterService : IChapterService
    {
        private SQLiteAsyncConnection _sqlClient => DependencyService.Get<ISqlRulesClient>().GetConnection();
        private IRulebookService _rulebookService => DependencyService.Get<IRulebookService>();
        
        public ChapterService()
        {}

        public async Task<List<Chapter>> GetChaptersFromCurrentRulebook()
        {
            var rulebook = await _rulebookService.GetCurrentRulebook();

            return await _sqlClient.Table<Chapter>().Where(o => o.RulebookId == rulebook.Id).ToListAsync();
        }
    }
}