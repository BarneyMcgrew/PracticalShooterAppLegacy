using System;
using System.Collections.Generic;
using RulebookConversionLibrary.Enums;
using RulebookConversionLibrary.Helpers;

namespace RulebookConversionLibrary.Services
{
    public class ConversionService
    {
        private readonly DatabaseService _databaseService;
        private readonly WebsiteService _websiteService;

        public ConversionService(string dbPath)
        {
            _databaseService = new DatabaseService(dbPath);
            _websiteService = new WebsiteService();
        }
        
        public void ConvertAllRulebooks()
        {
            Console.WriteLine("Converting rules from PDF to Text...");

            // Language -> Discipline -> Revision -> string[]
            var rulesTextDictionary = new Dictionary<Language, Dictionary<Discipline, Dictionary<string, string[]>>>();

            foreach (Language language in Enum.GetValues(typeof(Language)))
            {
                rulesTextDictionary.Add(language, new Dictionary<Discipline, Dictionary<string, string[]>>());

                foreach (Discipline rulebook in Enum.GetValues(typeof(Discipline)))
                {
                    // Get all revisions for this rulebook/language
                    var revisionTexts = PdfPigHelper.ConvertRulesPdfToText(rulebook, language); // returns Dictionary<string, string[]>
                    rulesTextDictionary[language].Add(rulebook, revisionTexts);
                }
            }

            Console.WriteLine("Converting Text to Object Rules...");

            var rulebooks = new List<Models.Rulebook>();
            var revisionDict = new Dictionary<string, int>(); // revisionCode -> RevisionId
            var revisions = new List<DataModels.Revision>();
            int nextRevisionId = 1;

            foreach (var language in rulesTextDictionary.Keys)
            {
                foreach (var rulebook in rulesTextDictionary[language].Keys)
                {
                    foreach (var revisionCode in rulesTextDictionary[language][rulebook].Keys)
                    {
                        // Parse revisionCode (MMYYYY) to DateTime (first day of month)
                        int month = int.Parse(revisionCode.Substring(0, 2));
                        int year = int.Parse(revisionCode.Substring(2, 4));
                        var revisionDate = new DateTime(year, month, 1);
                        if (!revisionDict.ContainsKey(revisionCode))
                        {
                            var revision = new DataModels.Revision
                            {
                                Id = nextRevisionId,
                                Code = revisionCode,
                                Date = revisionDate
                            };
                            revisions.Add(revision);
                            revisionDict[revisionCode] = nextRevisionId;
                            nextRevisionId++;
                        }
                        var text = rulesTextDictionary[language][rulebook][revisionCode];
                        var rules = RulesHelper.ConvertRulesFromText(rulebook, language, text);
                        var rb = new Models.Rulebook
                        {
                            Discipline = rulebook,
                            Language = language,
                            RevisionId = revisionDict[revisionCode],
                            Chapters = rules.Chapters,
                            Appendices = rules.Appendices,
                            Glossaries = rules.Glossaries,
                            Indices = rules.Indices
                        };
                        rulebooks.Add(rb);
                    }
                }
            }

            Console.WriteLine("Creating SqLite Database...");

            _databaseService.CreateDatabaseAndDefaultTables();
            _databaseService.CreateDefaultViews();
            
            Console.WriteLine("Populating SqLite Database...");
            _databaseService.PopulateDatabaseTables(rulebooks, revisions);
            
            Console.WriteLine("Creating Website Preview...");

            _websiteService.CreateWebsite(rulebooks);
        }
    }
}