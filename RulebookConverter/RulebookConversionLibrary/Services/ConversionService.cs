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
            
            var rulesTextDictionary = new Dictionary<Language, Dictionary<Discipline, string[]>>();

            foreach (Language language in Enum.GetValues(typeof(Language)))
            {
                rulesTextDictionary.Add(language, new Dictionary<Discipline, string[]>());
                
                foreach (Discipline rulebook in Enum.GetValues(typeof(Discipline)))
                {
                    var text = PdfPigHelper.ConvertRulesPdfToText(rulebook, language);
                
                    rulesTextDictionary[language].Add(rulebook, text);
                }
            }
            
            Console.WriteLine("Converting Text to Object Rules...");

            var rulebooks = new List<Models.Rulebook>();

            foreach (var language in rulesTextDictionary.Keys)
            {
                foreach (var rulebook in rulesTextDictionary[language].Keys)
                {
                    var text = rulesTextDictionary[language][rulebook];

                    var rules = RulesHelper.ConvertRulesFromText(rulebook, language, text);
                    
                    rulebooks.Add(rules);
                }
            }

            Console.WriteLine("Creating SqLite Database...");

            _databaseService.CreateDatabaseAndDefaultTables();
            _databaseService.CreateDefaultViews();
            
            Console.WriteLine("Populating SqLite Database...");
            
            _databaseService.PopulateDatabaseTables(rulebooks);
            
            Console.WriteLine("Creating Website Preview...");

            _websiteService.CreateWebsite(rulebooks);
        }
    }
}