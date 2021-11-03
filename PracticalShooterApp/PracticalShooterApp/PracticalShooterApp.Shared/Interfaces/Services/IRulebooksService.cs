using System.Collections.Generic;
using PracticalShooterApp.Shared.DataModels;
using PracticalShooterApp.Shared.Enums;

namespace PracticalShooterApp.Shared.Interfaces.Services
{
    public interface IRulebooksService
    {
        List<Glossary> SearchGlossaries(string searchCriteria);

        List<Appendix> SearchAppendices(string searchCriteria);

        List<Rule> SearchRules(string searchCriteria);
        
        List<Glossary> GetGlossaries();

        List<Appendix> GetAppendices();

        List<Chapter> GetChapters();

        List<Section> GetSections(int chapterId);

        List<Rule> GetRules(int sectionId);
    }
}