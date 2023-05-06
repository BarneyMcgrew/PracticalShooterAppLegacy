using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using RulebookConversionLibrary.Enums;
using RulebookConversionLibrary.Models;
using UglyToad.PdfPig.Fonts.AdobeFontMetrics;

namespace RulebookConversionLibrary.Helpers
{
    public static class RulesHelper
    {
        private static Regex _chapterRegex = new Regex(@"^CHAPTER\s(?<numeric>\d+):\s?(?<name>.+)?", RegexOptions.Compiled);
        private static Regex _sectionRegex = new Regex(@"^(?<numeric>\d+\.\d+)\s(?<name>.+)", RegexOptions.Compiled);
        private static Regex _ruleRegex = new Regex(@"^(?<numeric>\d+\.\d+\.\d+)\s((?<name>((\w+)\s)+)(-|–|�)\s)?(?<description>.+)", RegexOptions.Compiled);
        private static Regex _subRuleRegex = new Regex(@"^(?<numeric>\d+\.\d+\.\d+\.\d+)\s((?<name>((\w+)\s)+)(-|–|�)\s)?(?<description>.+)", RegexOptions.Compiled);
        private static Regex _numericRegex = new Regex(@"^(?<chapter>\d+)(\.(?<section>\d+)(\.(?<rule>\d+)(\.(?<subrule>\d+))?)?)?", RegexOptions.Compiled);
        private static Regex _appendixRegex = new Regex(@"APPENDIX\s(?<name>\S+\d?)\:(?<description>.+)?", RegexOptions.Compiled);
        private static Regex _glossaryRegex = new Regex(@"(?<key>.+[^\.])\s?\.{5,}(?<value>(.+))", RegexOptions.Compiled);
        private static Regex _glossarySplitRegex = new Regex(@"(?<=[^\.]\.)(\s|\n{2})", RegexOptions.Compiled);
        
        public static Models.Rulebook ConvertRulesFromText(Discipline disciplineType, Language languageType, string[] text)
        {
            var extractedObjects = ConvertToRules(text);

            var rulebook = MapRuleObjects(extractedObjects, disciplineType, languageType);
            
            return rulebook;
        }
        
        private static Rulebook MapRuleObjects(ConversionResult extractedObjects, Discipline disciplineType, Language languageType)
        {
            var rulebook = new Models.Rulebook()
            {
                Discipline = disciplineType,
                Language = languageType,
                Chapters = new List<Chapter>(),
                Appendices = extractedObjects.Appendices,
                Glossaries = extractedObjects.Glossaries
            };
            
            var subRules = extractedObjects.SubRules;
            var rulesDictionary = extractedObjects.Rules.ToDictionary(o => o.Numeric, p => p);
            var sectionsDictionary = extractedObjects.Sections.ToDictionary(o => o.Numeric, p => p);
            var chaptersDictionary = extractedObjects.Chapters.ToDictionary(o => o.Numeric, p => p);

            foreach (var subRule in subRules)
            {
                var numericRegex = _numericRegex.Match(subRule.Numeric);

                var ruleNumeric = $"{numericRegex.Groups["chapter"].Value}.{numericRegex.Groups["section"].Value}.{numericRegex.Groups["rule"].Value}";

                rulesDictionary[ruleNumeric].SubRules.Add(subRule);
            }

            foreach (var rule in rulesDictionary.Values)
            {
                var numericRegex = _numericRegex.Match(rule.Numeric);

                var sectionNumeric = $"{numericRegex.Groups["chapter"].Value}.{numericRegex.Groups["section"].Value}";

                sectionsDictionary[sectionNumeric].Rules.Add(rule);
            }

            foreach (var section in sectionsDictionary.Values)
            {
                var numericRegex = _numericRegex.Match(section.Numeric);

                var chapterNumeric = $"{numericRegex.Groups["chapter"].Value}";

                chaptersDictionary[chapterNumeric].Sections.Add(section);
            }
            
            foreach (var chapter in chaptersDictionary.Values)
            {
                rulebook.Chapters.Add(chapter);
            }

            return rulebook;
        }
        
        private static ConversionResult ConvertToRules(string[] fileArray)
        {

            var extractedChapters = new List<Chapter>();
            var extractedSections = new List<Section>();
            var extractedRules = new List<Rule>();
            var extractedSubRules = new List<SubRule>();
            var extractedAppendices = new List<Appendix>();
            var extractedGlossaries = new List<Glossary>();

            var lastLineWasChapter = false;
            var lastLineWasSection = false;
            var lastLineWasAppendix = false;
            var appendixStarted = false;
            var lastLineWasGlossary = false;
            var lastLineWasRule = false;
            var lastLineWasSubRule = false;
            var indexReached = false;

            foreach (var rawLine in fileArray)
            {
                // New bug fix for lines starting with whitespace
                var line = rawLine.Trim();
                
                // Check for Empty Line

                if (string.IsNullOrWhiteSpace(line)) continue;
                
                // Check for Index Line - we don't want indices (we will auto-generate in future)
                
                if (line.Contains("INDEX") || indexReached)
                {
                    indexReached = true;
                    continue;
                }

                // Check for Chapter Line

                var chapterRegex = _chapterRegex.Match(line);

                if (chapterRegex.Success)
                {
                    extractedChapters.Add(new Chapter()
                    {
                        Numeric = chapterRegex.Groups["numeric"].Value.Trim(),
                        Name = chapterRegex.Groups["name"].Value.Trim(),
                        Description = chapterRegex.Groups["description"].Value.Trim()
                    });

                    lastLineWasChapter = true;
                    lastLineWasSection = false;
                    lastLineWasAppendix = false;
                    lastLineWasGlossary = false;
                    lastLineWasRule = false;
                    lastLineWasSubRule = false;

                    continue;
                }
                
                // Check for Appendix Line

                var appendixRegex = _appendixRegex.Match(line);

                if (appendixRegex.Success)
                {
                    extractedAppendices.Add(new Appendix()
                    {
                        Name = appendixRegex.Groups["name"].Value.Trim(),
                        Description = appendixRegex.Groups["description"].Value.Trim(),
                        MetaDesciption = $"{appendixRegex.Value} "
                    });

                    lastLineWasChapter = false;
                    lastLineWasSection = false;
                    lastLineWasAppendix = true;
                    appendixStarted = true;
                    lastLineWasGlossary = false;
                    lastLineWasRule = false;
                    lastLineWasSubRule = false;

                    continue;
                }
                
                // Check for Glossary Line

                var glossaryRegex = _glossaryRegex.Match(line);

                if (glossaryRegex.Success && !appendixStarted)
                {
                    var glossarySplitRegex = _glossarySplitRegex.Match(line);

                    foreach (var subLine in _glossarySplitRegex.Split(line))
                    {
                        var subGlossaryRegex = _glossaryRegex.Match(subLine);

                        if (string.IsNullOrWhiteSpace(subGlossaryRegex.Groups["key"].Value))
                        {
                            extractedGlossaries.Last().Value += $" {subLine}";
                        }
                        else
                        {
                            extractedGlossaries.Add(new Glossary()
                            {
                                Key = subGlossaryRegex.Groups["key"].Value.Trim(),
                                Value = subGlossaryRegex.Groups["value"].Value.Trim()
                            });
                        }
                    }

                    lastLineWasChapter = false;
                    lastLineWasSection = false;
                    lastLineWasAppendix = false;
                    lastLineWasGlossary = true;
                    lastLineWasRule = false;
                    lastLineWasSubRule = false;

                    continue;
                }

                // Check for Section Line

                var sectionRegex = _sectionRegex.Match(line);

                if (sectionRegex.Success && !appendixStarted)
                {
                    extractedSections.Add(new Section()
                    {
                        Numeric = sectionRegex.Groups["numeric"].Value.Trim(),
                        Name = sectionRegex.Groups["name"].Value.Trim(),
                        Description = sectionRegex.Groups["description"].Value.Trim()
                    });

                    lastLineWasChapter = false;
                    lastLineWasSection = true;
                    lastLineWasAppendix = false;
                    lastLineWasGlossary = false;
                    lastLineWasRule = false;
                    lastLineWasSubRule = false;

                    continue;
                }

                // Check for Rule Line

                var ruleRegex = _ruleRegex.Match(line);

                if (ruleRegex.Success)
                {
                    extractedRules.Add(new Rule()
                    {
                        Numeric = ruleRegex.Groups["numeric"].Value.Trim(),
                        Name = ruleRegex.Groups["name"].Value.Trim(),
                        Description = ruleRegex.Groups["description"].Value.Trim()
                    });

                    lastLineWasChapter = false;
                    lastLineWasSection = false;
                    lastLineWasAppendix = false;
                    lastLineWasRule = true;
                    lastLineWasSubRule = false;

                    continue;
                }

                // Check for SubRule Line

                var subRuleRegex = _subRuleRegex.Match(line);

                if (subRuleRegex.Success)
                {
                    extractedSubRules.Add(new SubRule()
                    {
                        Numeric = subRuleRegex.Groups["numeric"].Value.Trim(),
                        Name = subRuleRegex.Groups["name"].Value.Trim(),
                        Description = subRuleRegex.Groups["description"].Value.Trim()
                    });

                    lastLineWasChapter = false;
                    lastLineWasSection = false;
                    lastLineWasAppendix = false;
                    lastLineWasRule = false;
                    lastLineWasSubRule = true;

                    continue;
                }
                
                // Check for Chapter Description

                if (lastLineWasChapter)
                {
                    extractedChapters.Last().Description = line;

                    lastLineWasChapter = false;
                    lastLineWasSection = false;
                    lastLineWasAppendix = false;
                    lastLineWasRule = false;
                    lastLineWasSubRule = false;
                }

                // Check for Section Description

                if (lastLineWasSection)
                {
                    extractedSections.Last().Description = line;

                    lastLineWasChapter = false;
                    lastLineWasSection = false;
                    lastLineWasAppendix = false;
                    lastLineWasRule = false;
                    lastLineWasSubRule = false;
                }

                // Check for Appendix Description and Metadescriptions
                
                if (lastLineWasAppendix)
                {
                    if (string.IsNullOrWhiteSpace(extractedAppendices.Last().Description))
                    {
                        extractedAppendices.Last().Description = line;
                    }
                    
                    extractedAppendices.Last().MetaDesciption += line;
                    
                    lastLineWasChapter = false;
                    lastLineWasSection = false;
                    // lastLineWasAppendix is kept as there could be more...
                    lastLineWasGlossary = false;
                    lastLineWasRule = false;
                    lastLineWasSubRule = false;
                }
                
                // Check for extra glossary descriptions

                if (lastLineWasGlossary)
                {
                    extractedGlossaries.Last().Value += $" {line}";
                    
                    lastLineWasChapter = false;
                    lastLineWasSection = false;
                    lastLineWasAppendix = false;
                    // lastLineWasGlossary is kept as there could be more...
                    lastLineWasRule = false;
                    lastLineWasSubRule = false;
                }
                
                // Check for extra rule descriptions

                if (lastLineWasRule)
                {
                    extractedRules.Last().Description += $" {Environment.NewLine}{Environment.NewLine}{line}";

                    lastLineWasChapter = false;
                    lastLineWasSection = false;
                    lastLineWasAppendix = false;
                    lastLineWasGlossary = false;
                    // lastLineWasRule is kept as there could be more
                    lastLineWasSubRule = false;
                }

                if (lastLineWasSubRule)
                {
                    extractedSubRules.Last().Description += $" {Environment.NewLine}{Environment.NewLine}{line}";
                    
                    lastLineWasChapter = false;
                    lastLineWasSection = false;
                    lastLineWasAppendix = false;
                    lastLineWasGlossary = false;
                    lastLineWasRule = false; 
                    // lastLineWasSubRule is kept as there could be more
                }
            }


            return new ConversionResult()
            {
                Chapters = extractedChapters,
                Sections = extractedSections,
                Rules = extractedRules,
                SubRules = extractedSubRules,
                Appendices = extractedAppendices,
                Glossaries = extractedGlossaries
            };
        }

        private class ConversionResult
        {
            public List<Chapter> Chapters;
            public List<Section> Sections;
            public List<Rule> Rules;
            public List<SubRule> SubRules;
            public List<Glossary> Glossaries;
            public List<Appendix> Appendices;
        }
    }
}