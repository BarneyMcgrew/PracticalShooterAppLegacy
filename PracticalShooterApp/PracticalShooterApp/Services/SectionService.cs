using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticalShooterApp.Clients;
using PracticalShooterApp.DataModels;
using PracticalShooterApp.Enums;
using SQLite;
using Xamarin.Forms;

namespace PracticalShooterApp.Services
{
    public class SectionService : ISectionService
    {
        private SQLiteAsyncConnection _sqlClient => DependencyService.Get<ISqlRulesClient>().GetConnection();
        private SettingsClient _settingsClient => DependencyService.Get<SettingsClient>();
        private IChapterService _chapterService => DependencyService.Get<IChapterService>();
        private ISectionService _sectionService => DependencyService.Get<ISectionService>();
        
        public SectionService()
        { }
        
        public async Task<List<Section>> GetSectionsByChapterId(int chapterId)
        {
            return await _sqlClient.Table<Section>().Where(o => o.ChapterId == chapterId).ToListAsync();
        }

        public async Task<Section> GetSectionBySectionId(int sectionId)
        {
            return await _sqlClient.Table<Section>().FirstAsync(o => o.Id == sectionId);
        }

        public async Task<Section> GetSectionBySectionNumeric(string numeric)
        {
            var chapters = await _chapterService.GetChaptersFromCurrentRulebook();

            var chapter = chapters.First(o => o.Numeric == numeric[0].ToString());

            var sections = await _sectionService.GetSectionsByChapterId(chapter.Id);

            var section = sections.FirstOrDefault(o => o.Numeric == numeric);

            return section;
        }
    }
}