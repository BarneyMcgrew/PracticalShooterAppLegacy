using PracticalShooterLibrary.ObjectClasses.Rules;
using PracticalShooterLibrary.ObjectClasses.Utilities;
using PracticalShooterLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static PracticalShooterLibrary.GlobalResources.GlobalEnums;

namespace PracticalShooterLibrary.StaticResources
{
    public class GlobalResourceCache
    {
        public static GlobalResourceCache Current = new GlobalResourceCache();

        public Dictionary<string, Guid> DisciplineNameDictionary;

        public Dictionary<Guid, Discipline> DisciplinesDictionary;

        public Dictionary<string, Appendices> AppendicesDictionary;

        public WebsiteCollection WebsitesCollection;

        public bool ResourcesLoaded { get; set; }

        public GlobalResourceCache() { }

        public void LoadTutorials()
        {
            ResourcesLoaded = true;

        }

        public void LoadRulebooks()
        {
            var rulebookReader = new RulebookReader();

            var rulebook = rulebookReader.ReadRulebookResource();

            DisciplineNameDictionary = rulebook.Disciplines.ToDictionary(o => Enum.GetName(typeof(DisciplineTypes), o.DisciplineType), o => o.DisciplineId);

            DisciplinesDictionary = rulebook.Disciplines.ToDictionary(o => o.DisciplineId, o => o);

            ResourcesLoaded = true;
        }

        public void LoadAppendices()
        {
            var appendicesReader = new AppendixReader();

            var appendixLibrary = appendicesReader.ReadAppendixResource();

            AppendicesDictionary = appendixLibrary.Disciplines.ToDictionary(o => Enum.GetName(typeof(DisciplineTypes), o.DisciplineType), o => o);

            ResourcesLoaded = true;
        }

        public void LoadCalculators()
        {
            ResourcesLoaded = true;

        }

        public void LoadParTimer()
        {
            ResourcesLoaded = true;

        }

        public void LoadWebsites()
        {
            var websiteReader = new WebsitesReader();

            var websites = websiteReader.ReadWebsitesResource();

            WebsitesCollection = websites;

            ResourcesLoaded = true;
        }

        public void LoadDrillsLibrary()
        {
            ResourcesLoaded = true;

        }

        public Discipline GetDiscipline()
        {
            var disciplineId = DisciplineNameDictionary[GlobalSettings.Current.SelectedDiscipline.Replace(" ", "")];

            return DisciplinesDictionary[disciplineId];
        }

        public Appendices GetAppendices()
        {
            return AppendicesDictionary[GlobalSettings.Current.SelectedDiscipline.Replace(" ", "")];
        }

        public Section GetSection(Guid referenceId)
        {
            var discipline = GetDiscipline();

            return discipline.Chapters.SelectMany(o => o.Sections).First(o => o.SectionId == referenceId);
        }

        public Section GetSectionByNumeric(string numeric)
        {
            var discipline = GetDiscipline();

            return discipline.Chapters.SelectMany(o => o.Sections).FirstOrDefault(o => o.Numeric == numeric);
        }

        public Rule GetRule(Guid referenceId)
        {
            var discipline = GetDiscipline();

            var rules = discipline.Chapters.SelectMany(o => o.Sections).SelectMany(u => u.Rules);

            return rules.First(o => o.RuleId == referenceId);
        }

        public SubRule GetSubRule(Guid referenceId)
        {
            var discipline = GetDiscipline();

            var subRules = discipline.Chapters.SelectMany(o => o.Sections).SelectMany(u => u.Rules).SelectMany(y => y.SubRules);

            return subRules.First(o => o.SubRuleId == referenceId);
        }

        public Glossary GetGlossary(Guid referenceId)
        {
            var discipline = GetDiscipline();

            return discipline.GlossaryList.Single(o => o.GlossaryId == referenceId);
        }        
    }
}
