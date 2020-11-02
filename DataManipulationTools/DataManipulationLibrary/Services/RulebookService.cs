using DataManipulationLibrary.Helpers;
using DataManipulationLibrary.ObjectClasses.Rules;
using IPSCCompanionLibrary.ObjectClasses.Rules;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms.Internals;
using static IPSCCompanionLibrary.GlobalResources.GlobalEnums;

namespace DataManipulationLibrary.Services
{
    public class RulebookService
    {
        private readonly string _filePath = AppDomain.CurrentDomain.BaseDirectory.Replace("IPSCCompanionApp\\DataManipulationTools\\DataUtilitiesApp\\bin\\Debug\\", "resources\\rulebooks\\");
        private readonly string _loadFile = "Rulebook.data";
        private readonly string _saveFile = "Rulebook.data";

        private OldRulebook _oldRulebook;

        private Rulebook _rulebook;
        
        public RulebookService() 
        {
            ExtractXMLFile();
            ConvertRulebook();
        }

        public Dictionary<Guid, string> GetDisciplines()
        {
            return _oldRulebook.Disciplines.ToDictionary(o => o.DisciplineId, o => o.DisciplineType.ToString());
        }

        public Dictionary<Guid, string> GetSections(Guid disciplineId)
        {
            return _oldRulebook.Disciplines.First(o => o.DisciplineId == disciplineId).Chapters.SelectMany(o => o.Sections).ToDictionary(o => o.SectionId, o => $"{o.Numeric} - {o.Name}");
        }

        public Dictionary<Guid, string> GetGlossaries(Guid DisciplineId)
        {
            return _oldRulebook.Disciplines.First(o => o.DisciplineId == DisciplineId).GlossaryList.ToDictionary(o => o.GlossaryId, o => o.Name);
        }

        private void ExtractXMLFile()
        {           
            var file = UtilityHelper.ReadStreamFromFile($"{_filePath}/{_loadFile}");

            StreamReader reader = new StreamReader(file);
            string text = reader.ReadToEnd();
            _oldRulebook = JsonConvert.DeserializeObject<OldRulebook>(text);
        }

        public void SaveXMLFile()
        {
            Debug.WriteLine("Outputting new rulebook file...");

            using (var xmlFile = UtilityHelper.GenerateStreamFromString(JsonConvert.SerializeObject(_rulebook, Formatting.Indented)))
            {
                UtilityHelper.CreateUpdateFile(_filePath, _saveFile, Encoding.UTF8.GetString(xmlFile.ToArray()));
            }

            Debug.WriteLine("SUCCESS");
        }

        private void ConvertRulebook()
        {
            _rulebook = new Rulebook()
            {
                Disciplines = new List<Discipline>()
            };

            _oldRulebook.Disciplines.ForEach((discipline) => 
            {
                Debug.WriteLine($"Discipline: {Enum.GetName(typeof(DisciplineTypes), discipline.DisciplineType)}");

                Debug.WriteLine($"Converting Discipline Properties");

                discipline = ConvertDisciplineProperties(discipline);

                Debug.WriteLine($"Converting Rule Span Properties");

                discipline = ConvertRuleSpanProperties(discipline);

                Debug.WriteLine($"Converting old discipline to new discipline");

                _rulebook.Disciplines.Add(ConvertFromOldToNew(discipline));
            });
        }

        private Discipline ConvertFromOldToNew(OldDiscipline oldDiscipline)
        {
            var discipline = new Discipline()
            {
                DisciplineId = oldDiscipline.DisciplineId,
                LanguageType = oldDiscipline.LanguageType,
                GlossaryList = new List<Glossary>(),
                Chapters = new List<Chapter>(),
                DisciplineType = oldDiscipline.DisciplineType
            };

            foreach (var glossary in oldDiscipline.GlossaryList)
            {
                discipline.GlossaryList.Add(new Glossary()
                {
                    GlossaryId = glossary.GlossaryId,
                    DisciplineId = glossary.DisciplineId,
                    Name = glossary.Name,
                    Definition = glossary.Definition
                });
            }

            foreach (var oldChapter in oldDiscipline.Chapters)
            {
                var chapter = new Chapter()
                {
                    ChapterId = oldChapter.ChapterId,
                    Name = oldChapter.Name,
                    Description = oldChapter.Description,
                    Numeric = oldChapter.Numeric,
                    DisciplineId = oldChapter.DisciplineId,
                    Sections = new List<Section>()
                };

                foreach (var oldSection in oldChapter.Sections)
                {
                    var section = new Section()
                    {
                        DisciplineId = oldSection.DisciplineId,
                        ChapterId = oldSection.ChapterId,
                        SectionId = oldSection.SectionId,
                        SearchableString = oldSection.SearchableString,
                        Name = oldSection.Name,
                        Numeric = oldSection.Numeric,
                        Description = oldSection.Description,
                        Rules = new List<Rule>()
                    };

                    foreach (var oldRule in oldSection.Rules)
                    {
                        var rule = new Rule()
                        {
                            DisciplineId = oldRule.DisciplineId,
                            SectionId = oldRule.SectionId,
                            SearchableString = oldRule.SearchableString,
                            Name = oldRule.Name,
                            Numeric = oldRule.Numeric,
                            RuleId = oldRule.RuleId,
                            Description = oldRule.Description,
                            SpanList = new List<RuleSpan>(),
                            SubRules = new List<SubRule>()
                        };

                        foreach (var oldRuleSpan in oldRule.SpanList)
                        {
                            var ruleSpan = new RuleSpan()
                            {
                                SpanType = oldRuleSpan.SpanType,
                                Text = oldRuleSpan.Text,
                                ReferenceId = oldRuleSpan.ReferenceId
                            };

                            rule.SpanList.Add(ruleSpan);
                        }

                        foreach (var oldSubRule in oldRule.SubRules)
                        {
                            var subRule = new SubRule()
                            {
                                DisciplineId = oldSubRule.DisciplineId,
                                RuleId = oldSubRule.RuleId,
                                SubRuleId = oldSubRule.SubRuleId,
                                Name = oldSubRule.Name,
                                Numeric = oldSubRule.Numeric,
                                Description = oldSubRule.Description,
                                SearchableString = oldSubRule.SearchableString,
                                SpanList = new List<RuleSpan>()
                            };

                            foreach (var oldRuleSpan in oldSubRule.SpanList)
                            {
                                var ruleSpan = new RuleSpan()
                                {
                                    SpanType = oldRuleSpan.SpanType,
                                    Text = oldRuleSpan.Text,
                                    ReferenceId = oldRuleSpan.ReferenceId
                                };

                                subRule.SpanList.Add(ruleSpan);
                            }

                            rule.SubRules.Add(subRule);
                        }

                        section.Rules.Add(rule);
                    }

                    chapter.Sections.Add(section);
                }

                discipline.Chapters.Add(chapter);
            }

            return discipline;
        }

        private OldDiscipline ConvertDisciplineProperties(OldDiscipline discipline)
        {
            if (!string.IsNullOrEmpty(discipline.Language))
                discipline.LanguageType = (LanguageTypes)Enum.Parse(typeof(LanguageTypes), discipline.Language);
            if (!string.IsNullOrEmpty(discipline.Name))
                discipline.DisciplineType = (DisciplineTypes)Enum.Parse(typeof(DisciplineTypes), discipline.Name);

            discipline.Language = string.Empty;
            discipline.Name = string.Empty;

            return discipline;
        }

        private OldDiscipline ConvertRuleSpanProperties(OldDiscipline discipline)
        {
            var ruleSpans = discipline.Chapters.SelectMany(o => o.Sections).SelectMany(o => o.Rules).SelectMany(o => o.SpanList);
            var subRuleSpans = discipline.Chapters.SelectMany(o => o.Sections).SelectMany(o => o.Rules).SelectMany(o => o.SubRules).SelectMany(o => o.SpanList);

            ruleSpans.ForEach((ruleSpan) => {
                switch (ruleSpan.SpanType)
                {
                    case SpanTypes.Normal:
                        break;
                    case SpanTypes.GlossaryTooltip:
                        if (ruleSpan.GlossaryId.HasValue)
                        {
                            ruleSpan.ReferenceId = ruleSpan.GlossaryId.Value;
                            ruleSpan.GlossaryId = null;
                        }
                        break;
                    case SpanTypes.RuleReference:
                        if (ruleSpan.RuleId.HasValue)
                        {
                            ruleSpan.ReferenceId = ruleSpan.RuleId.Value;
                            ruleSpan.RuleId = null;
                        }
                        break;
                    case SpanTypes.SubRuleReference:
                        if (ruleSpan.SubRuleId.HasValue)
                        {
                            ruleSpan.ReferenceId = ruleSpan.SubRuleId.Value;
                            ruleSpan.SubRuleId = null;
                        }
                        break;
                    case SpanTypes.SectionReference:
                        // Weird data error. Guid is stored in rule id not section id
                        if (ruleSpan.RuleId.HasValue)
                        {
                            ruleSpan.ReferenceId = ruleSpan.RuleId.Value;
                            ruleSpan.SectionId = null;
                        }
                        break;
                    default:
                        break;
                }
            });

            subRuleSpans.ForEach((ruleSpan) =>
            {
                switch (ruleSpan.SpanType)
                {
                    case SpanTypes.Normal:
                        break;
                    case SpanTypes.GlossaryTooltip:
                        if (ruleSpan.GlossaryId.HasValue)
                        {
                            ruleSpan.ReferenceId = ruleSpan.GlossaryId.Value;
                            ruleSpan.GlossaryId = null;
                        }
                        break;
                    case SpanTypes.RuleReference:
                        if (ruleSpan.RuleId.HasValue)
                        {
                            ruleSpan.ReferenceId = ruleSpan.RuleId.Value;
                            ruleSpan.RuleId = null;
                        }
                        break;
                    case SpanTypes.SubRuleReference:
                        if (ruleSpan.SubRuleId.HasValue)
                        {
                            ruleSpan.ReferenceId = ruleSpan.SubRuleId.Value;
                            ruleSpan.SubRuleId = null;
                        }
                        break;
                    case SpanTypes.SectionReference:
                        // Weird data error. Guid is stored in rule id not section id
                        if (ruleSpan.RuleId.HasValue)
                        {
                            ruleSpan.ReferenceId = ruleSpan.RuleId.Value;
                            ruleSpan.SectionId = null;
                        }
                        break;
                    default:
                        break;
                }
            });

            return discipline;
        }
    }
}
