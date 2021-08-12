using System;
using System.Collections.Generic;
using System.Text;
using PracticalShooterApp.Shared.DataModels;
using PracticalShooterApp.Shared.Enums;

namespace PracticalShooterApp.Shared.Interfaces.Services
{
    public interface IDatabaseService
    {
        List<Glossary> SearchGlossaries(string searchCriteria, Discipline discipline, Language language);

        List<Appendix> SearchAppendices(string searchCriteria, Discipline discipline, Language language);

        List<Rule> SearchRules(string searchCriteria, Discipline discipline, Language language);
        
        List<Glossary> GetGlossaries(Discipline discipline, Language language);

        List<Appendix> GetAppendices(Discipline discipline, Language language);

        List<Chapter> GetChapters(Discipline discipline, Language language);

        List<Section> GetSections(int chapterId);

        List<Rule> GetRules(int sectionId);
    }
}
