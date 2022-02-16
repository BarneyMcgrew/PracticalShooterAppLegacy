using System.Collections.Generic;
using System.Threading.Tasks;
using PracticalShooterApp.Clients;
using PracticalShooterApp.DataModels;
using SQLite;
using Xamarin.Forms;

namespace PracticalShooterApp.Services
{
    public class SectionService : ISectionService
    {
        private SQLiteAsyncConnection _sqlClient => DependencyService.Get<ISqlRulesClient>().GetConnection();
        
        public SectionService()
        { }
        
        public async Task<List<Section>> GetSectionsByChapterId(int chapterId)
        {
            return await _sqlClient.Table<Section>().Where(o => o.ChapterId == chapterId).ToListAsync();
        }
    }
}