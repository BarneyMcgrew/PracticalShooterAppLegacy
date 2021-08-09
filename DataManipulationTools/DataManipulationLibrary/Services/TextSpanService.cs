using DataManipulationLibrary.ObjectClasses.Rules;
using PracticalShooterLibrary.ObjectClasses.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using static PracticalShooterLibrary.GlobalResources.GlobalEnums;

namespace DataManipulationLibrary.Services
{
    public class TextSpanService
    {
        private List<Discipline> _allDisciplines = new List<Discipline>();
        private List<Section> _allSections = new List<Section>();
        private List<Rule> _allRules = new List<Rule>();
        private List<SubRule> _allSubRules = new List<SubRule>();
        private List<Glossary> _allGlossaries = new List<Glossary>();

        public TextSpanService(Rulebook rulebook)
        {
            _allDisciplines = rulebook.Disciplines;
            _allSections = _allDisciplines.SelectMany(o => o.Chapters).SelectMany(o => o.Sections).ToList();
            _allRules = _allSections.SelectMany(o => o.Rules).ToList();
            _allSubRules = _allRules.SelectMany(o => o.SubRules).ToList();
            _allGlossaries = _allDisciplines.SelectMany(o => o.GlossaryList).Select(o => o).ToList();
        }

        public List<OldRuleSpan> CreateSpanList(string paragraph, Guid disciplineId)
        {
            var spanList = new List<OldRuleSpan>();

            var sectionList = _allSections.Where(o => o.DisciplineId == disciplineId);
            var glossaryList = _allGlossaries.Where(o => o.DisciplineId == disciplineId);
            var ruleList = _allRules.Where(o => o.DisciplineId == disciplineId);
            var subRuleList = _allSubRules.Where(o => o.DisciplineId == disciplineId);
                        
            foreach (var subRule in subRuleList.Reverse())
            {
                var regex = new Regex($"(?<=\\s|^)({subRule.Numeric.Replace(".", "\\.")})(?=\\s|\\S\\s|\\)|\\S?$)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

                var match = regex.Match(paragraph);

                if (match.Success)
                {
                    paragraph = regex.Replace(paragraph, $"|{match.Value}|");
                }
            }

            foreach (var rule in ruleList.Reverse())
            {
                if (string.IsNullOrWhiteSpace(rule.Numeric))
                    continue;

                var regex = new Regex($"(?<=\\s|^)({rule.Numeric.Replace(".", "\\.")})(?=\\s|\\S\\s|\\)|\\S?$)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

                var match = regex.Match(paragraph);

                if (match.Success)
                {
                    paragraph = regex.Replace(paragraph, $"|{match.Value}|");
                }
            }

            foreach (var glossaryItem in glossaryList)
            {
                var regex = new Regex($"(?<=\\W|^)((({glossaryItem.Name.Replace("(s)", "(s?)").Replace(" (", "))|(((").Replace(" / ", ")|(").Trim()})))(?=\\W|$)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

                var match = regex.Match(paragraph);

                if (match.Success)
                {
                    paragraph = regex.Replace(paragraph, $"|{match.Value}|");
                }
            }

            foreach (var section in sectionList.Reverse())
            {
                var regex = new Regex($"(?<=\\s|^)({section.Numeric.Replace(".", "\\.")})(?=\\s|\\S\\s|\\)|\\S?$)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

                var match = regex.Match(paragraph);

                if (match.Success)
                {
                    paragraph = regex.Replace(paragraph, $"|{match.Value}|");
                }
            }

            var externalRegex = new Regex($"<(?<type>((External)|(Articles)|(Drills)):\"(?<ref>.+)\",\"(?<name>.+)\")>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            foreach (Match match in externalRegex.Matches(paragraph))
            {
                paragraph = externalRegex.Replace(paragraph, $"|{match.Value}|");
            }

            var descriptionArray = paragraph.Split('|');

            foreach (var split in descriptionArray)
            {
                var splitAdded = false;

                foreach (var subRule in subRuleList.Reverse())
                {
                    var regex = new Regex($"(?<=\\s|^)({subRule.Numeric.Replace(".", "\\.")})(?=\\s|\\S\\s|\\)|\\S?$)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

                    var match = regex.Match(split);

                    if (match.Success)
                    {
                        spanList.Add(new OldRuleSpan()
                        {
                            Text = split,
                            SpanType = SpanTypes.SubRuleReference,
                            SubRuleId = subRule.SubRuleId
                        });

                        splitAdded = true;
                        break;
                    }
                }

                if (splitAdded)
                    continue;

                foreach (var rule in ruleList.Reverse())
                {
                    if (string.IsNullOrWhiteSpace(rule.Numeric))
                        continue;

                    var regex = new Regex($"(?<=\\s|^)({rule.Numeric.Replace(".", "\\.")})(?=\\s|\\S\\s|\\)|\\S?$)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

                    var match = regex.Match(split);

                    if (match.Success)
                    {
                        spanList.Add(new OldRuleSpan()
                        {
                            Text = split,
                            SpanType = SpanTypes.RuleReference,
                            RuleId = rule.RuleId
                        });

                        splitAdded = true;
                        break;
                    }
                }

                if (splitAdded)
                    continue;

                foreach (var section in sectionList.Reverse())
                {
                    if (string.IsNullOrWhiteSpace(section.Numeric))
                        continue;

                    var regex = new Regex($"(?<=\\s|^)({section.Numeric.Replace(".", "\\.")})(?=\\s|\\S\\s|\\)|\\S?$)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

                    var match = regex.Match(split);

                    if (match.Success)
                    {
                        spanList.Add(new OldRuleSpan()
                        {
                            Text = split,
                            SpanType = SpanTypes.SectionReference,
                            RuleId = section.SectionId
                        });

                        splitAdded = true;
                        break;
                    }
                }

                if (splitAdded)
                    continue;

                foreach (var glossaryItem in glossaryList)
                {
                    var regex = new Regex($"(?<=\\W|^)((({glossaryItem.Name.Replace("(s)", "(s?)").Replace(" (", "))|(((").Replace(" / ", ")|(").Trim()})))(?=\\W|$)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

                    var match = regex.Match(split);

                    if (match.Success)
                    {
                        spanList.Add(new OldRuleSpan()
                        {
                            Text = split,
                            SpanType = SpanTypes.GlossaryTooltip,
                            GlossaryId = glossaryItem.GlossaryId
                        });

                        splitAdded = true;
                        break;
                    }
                }

                if (splitAdded)
                    continue;

                var extMatch = externalRegex.Match(split);

                if (extMatch.Success)
                {
                    var extRef = extMatch.Groups["type"];

                    switch (extRef.Value)
                    {
                        case "External":
                            break;
                        case "Articles":
                            break;
                        case "Drills":
                            break;
                        default:
                            break;
                    }
                }

                if (splitAdded)
                    continue;

                // NO Reference Found
                spanList.Add(new OldRuleSpan()
                {
                    Text = split,
                    SpanType = SpanTypes.Normal
                });
            }

            return spanList;
        }
    }
}
