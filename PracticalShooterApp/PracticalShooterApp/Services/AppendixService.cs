using System.Collections.Generic;
using System.Threading.Tasks;
using PracticalShooterApp.Clients;
using PracticalShooterApp.DataModels;
using SQLite;
using Xamarin.Forms;

namespace PracticalShooterApp.Services
{
    public class AppendixService : IAppendixService
    {
        private SQLiteAsyncConnection _sqlClient => DependencyService.Get<ISqlRulesClient>().GetConnection();
        private IRulebookService _rulebookService => DependencyService.Get<IRulebookService>();
        
        public AppendixService()
        {}

        public async Task<List<Appendix>> GetAllAppendices()
        {
            return await _sqlClient.Table<Appendix>().ToListAsync();
        }

        public async Task<List<Appendix>> GetAppendicesFromCurrentRulebook()
        {
            var rulebook = await _rulebookService.GetCurrentRulebook();

            return await _sqlClient.Table<Appendix>().Where(o => o.RulebookId == rulebook.Id).ToListAsync();
        }
    }
}