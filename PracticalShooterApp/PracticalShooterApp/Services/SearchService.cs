using System.Collections.Generic;
using System.Threading.Tasks;
using PracticalShooterApp.Clients;
using PracticalShooterApp.DataModels;
using PracticalShooterApp.Models;
using SQLite;
using Xamarin.Forms;
using System;
using System.Linq;
using PracticalShooterApp.Extensions;
using System.ComponentModel.DataAnnotations;

namespace PracticalShooterApp.Services
{
    public class SearchService : ISearchService
    {
        private IGlossaryService _glossaryService => DependencyService.Get<GlossaryService>();
        private IRulesService _rulesService => DependencyService.Get<RulesService>();
        private ISectionService _sectionService => DependencyService.Get<SectionService>();
        private IChapterService _chapterService => DependencyService.Get<ChapterService>();
        private IRulebookService _rulebookService => DependencyService.Get<RulebookService>();
        private IAppendixService _appendixService => DependencyService.Get<AppendixService>();
        
        public SearchService()
        { }
        
        public async Task<List<SearchModel>> SearchRules(string searchParameters)
        {
            var searchParametersInvariant = searchParameters.ToLowerInvariant();
            
            var results = new List<SearchModel>();
            
            var selectedRulebook = await _rulebookService.GetCurrentRulebook();

            var glossaries = await _glossaryService.GetAllGlossaries();

            var glossaryResults = glossaries.Where(o => o.Key.ToLowerInvariant().Contains(searchParametersInvariant) ||
                                                        o.Value.ToLowerInvariant().Contains(searchParametersInvariant));

            foreach (var glossary in glossaryResults)
            {
                if (selectedRulebook.Id != glossary.RulebookId)
                    continue;
                
                var rulebook = await _rulebookService.GetRulebookbyRulebookId(glossary.RulebookId);

                var searchModel = new SearchModel()
                {
                    Id = glossary.Id,
                    Numeric = glossary.Key,
                    Description = glossary.Value,
                    SubRules = new List<SubRuleModel>(),
                    RulebookName = rulebook.Discipline.GetAttribute<DisplayAttribute>().Name,
                    SectionName = "Glossary",
                    IsNotDescription = true,
                    IsNotSubRule = true
                };
                
                results.Add(searchModel);
            }

            var rules = await _rulesService.GetAllRules();

            var rulesResults = rules.Where(o => o.Numeric.ToLowerInvariant().Contains(searchParametersInvariant) ||
                                           o.Name.ToLowerInvariant().Contains(searchParametersInvariant) ||
                                           o.Description.ToLowerInvariant().Contains(searchParametersInvariant));

            var subRules = await _rulesService.GetAllSubRules();

            var subrulesResults = subRules.Where(o => o.Numeric.ToLowerInvariant().Contains(searchParametersInvariant) ||
                                           o.Name.ToLowerInvariant().Contains(searchParametersInvariant) ||
                                           o.Description.ToLowerInvariant().Contains(searchParametersInvariant));
            
            foreach (var rule in rulesResults)
            {
                var section = await _sectionService.GetSectionBySectionId(rule.SectionId);
                var chapter = await _chapterService.GetChapterByChapterId(section.ChapterId);
                var rulebook = await _rulebookService.GetRulebookbyRulebookId(chapter.RulebookId);
                
                if (selectedRulebook.Id != rulebook.Id)
                    continue;

                var searchModel = new SearchModel()
                {
                    Id = rule.Id,
                    Name = rule.Name,
                    Description = rule.Description,
                    Numeric = rule.Numeric,
                    SubRules = new List<SubRuleModel>(),
                    ChapterName = chapter.Name,
                    RulebookName = rulebook.Discipline.GetAttribute<DisplayAttribute>().Name,
                    IsNotDescription = true,
                    IsNotSubRule = true,
                    SectionId = section.Id,
                    SectionName = section.Name
                };

                results.Add(searchModel);
            }

            foreach (var subRule in subrulesResults)
            {
                var rule = await _rulesService.GetRuleByRuleId(subRule.RuleId);
                var section = await _sectionService.GetSectionBySectionId(rule.SectionId);
                var chapter = await _chapterService.GetChapterByChapterId(section.ChapterId);
                var rulebook = await _rulebookService.GetRulebookbyRulebookId(chapter.RulebookId);

                if (selectedRulebook.Id != rulebook.Id)
                    continue;

                var searchModel = new SearchModel()
                {
                    Id = subRule.Id,
                    Name = subRule.Name,
                    Description = subRule.Description,
                    Numeric = subRule.Numeric,
                    SubRules = new List<SubRuleModel>(),
                    ChapterName = chapter.Name,
                    RulebookName = rulebook.Discipline.GetAttribute<DisplayAttribute>().Name,
                    IsNotDescription = true,
                    IsNotSubRule = true,
                    SectionId = section.Id,
                    SectionName = section.Name
                };

                results.Add(searchModel);
            }

            var appendices = await _appendixService.GetAllAppendices();

            var appendixResults = appendices.Where(o => o.Name.ToLowerInvariant().Contains(searchParametersInvariant) ||
                                                        o.Description.ToLowerInvariant()
                                                            .Contains(searchParametersInvariant) ||
                                                        o.MetaDescription.ToLowerInvariant()
                                                            .Contains(searchParametersInvariant));

            foreach (var appendix in appendixResults)
            {
                var rulebook = await _rulebookService.GetRulebookbyRulebookId(appendix.RulebookId);
                
                if (selectedRulebook.Id != rulebook.Id)
                    continue;

                var searchModel = new SearchModel()
                {
                    Id = appendix.Id,
                    Numeric = $"{appendix.Name} - {appendix.Description}",
                    RulebookName = rulebook.Discipline.GetAttribute<DisplayAttribute>().Name,
                    IsNotDescription = true,
                    IsNotSubRule = true,
                    SectionName = "Appendices"
                };
                
                results.Add(searchModel);
            }

            return results;
        }
    }
}