using System.Collections.Generic;
using PracticalShooterApp.Shared.DataModels;
using PracticalShooterApp.Shared.Enums;
using PracticalShooterApp.Shared.Interfaces.Services;

namespace PracticalShooterApp.Shared.Services
{
    public class RulebooksService : IRulebooksService
    {
        private readonly DatabaseService _databaseService;
        private readonly SettingsService _settingsService;

        private Language _language;
        private Discipline _discipline;

        public RulebooksService()
        {
            _databaseService = new DatabaseService();
            _settingsService = new SettingsService();
        }

        private void GetLatestSettings()
        {
            _language = _settingsService.DefaultLanguage;
            _discipline = _settingsService.DefaultDiscipline;
        }

        public List<Glossary> SearchGlossaries(string searchCriteria)
        {
            GetLatestSettings();

            return _databaseService.SearchGlossaries(searchCriteria, _discipline, _language);
        }

        public List<Appendix> SearchAppendices(string searchCriteria)
        {
            GetLatestSettings();

            return _databaseService.SearchAppendices(searchCriteria, _discipline, _language);
        }

        public List<Rule> SearchRules(string searchCriteria)
        {
            GetLatestSettings();

            return _databaseService.SearchRules(searchCriteria, _discipline, _language);
        }

        public List<Glossary> GetGlossaries()
        {
            GetLatestSettings();

            return _databaseService.GetGlossaries(_discipline, _language);
        }

        public List<Appendix> GetAppendices()
        {
            GetLatestSettings();

            return _databaseService.GetAppendices(_discipline, _language);
        }

        public List<Chapter> GetChapters()
        {
            GetLatestSettings();

            return _databaseService.GetChapters(_discipline, _language);
        }

        public List<Section> GetSections(int chapterId)
        {
            return _databaseService.GetSections(chapterId);
        }

        public List<Rule> GetRules(int sectionId)
        {
            return _databaseService.GetRules(sectionId);
        }
    }
}