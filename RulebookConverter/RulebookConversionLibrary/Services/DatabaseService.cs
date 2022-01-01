using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using RulebookConversionLibrary.DataModels;
using RulebookConversionLibrary.Enums;
using RulebookConversionLibrary.Helpers;
using RulebookConversionLibrary.Models;
using SQLite;
using Appendix = RulebookConversionLibrary.Models.Appendix;
using Chapter = RulebookConversionLibrary.Models.Chapter;
using Glossary = RulebookConversionLibrary.DataModels.Glossary;
using Rulebook = RulebookConversionLibrary.DataModels.Rulebook;

namespace RulebookConversionLibrary.Services
{
    public class DatabaseService
    {
        private readonly string _databaseName;

        public DatabaseService(string databaseName)
        {
            _databaseName = databaseName;
        }

        public void CreateDatabaseAndDefaultTables()
        {
            using (var con = new SQLiteConnection(_databaseName))
            {
                con.Execute(@"
                    CREATE TABLE Rulebook (
                        Id integer,
                        Discipline integer,
                        Language integer,
                        PRIMARY KEY (Id)                        
                    );
                ");

                con.Execute(@"
                    CREATE TABLE Appendix (
                        Id integer,
                        Name varchar,
                        Description varchar,
                        MetaDescription varchar,
                        RulebookId integer,
                        PRIMARY KEY (Id),
                        FOREIGN KEY (RulebookId) REFERENCES Rulebook(Id)
                    );
                ");

                con.Execute(@"
                    CREATE TABLE Glossary (
                        Id integer,
                        Key varchar,
                        Value varchar,
                        RulebookId integer,
                        PRIMARY KEY (Id),
                        FOREIGN KEY (RulebookId) REFERENCES Rulebook(Id)  
                    );
                ");

                con.Execute(@"
                    CREATE TABLE Chapter (
                        Id integer,
                        Numeric varchar,
                        Name varchar,
                        Description varchar,
                        RulebookId integer,
                        PRIMARY KEY (Id),
                        FOREIGN KEY (RulebookId) REFERENCES Rulebook(Id)
                    );
                ");

                con.Execute(@"
                    CREATE TABLE Section (
                        Id integer,
                        Numeric varchar,
                        Name varchar,
                        Description varchar,
                        ChapterId integer,
                        PRIMARY KEY (Id),
                        FOREIGN KEY (ChapterId) REFERENCES Chapter(Id)
                    );
                ");

                con.Execute(@"
                    CREATE TABLE Rule (
                        Id integer,
                        Numeric varchar,
                        Name varchar,
                        Description varchar,
                        SectionId integer,
                        PRIMARY KEY (Id),
                        FOREIGN KEY (SectionId) REFERENCES Section(Id)  
                    );
                ");

                con.Execute(@"
                    CREATE TABLE SubRule (
                        Id integer,
                        Numeric varchar,
                        Name varchar,
                        Description varchar,
                        RuleId integer,
                        PRIMARY KEY (Id),
                        FOREIGN KEY (RuleId) REFERENCES Rule(Id)
                    );
                ");
            }
        }

        public void CreateDefaultViews()
        {
            
        }

        public void PopulateDatabaseTables(List<Models.Rulebook> rulebooks)
        {
            var rulebookList = new List<DataModels.Rulebook>();
            var glossaryList = new List<DataModels.Glossary>();
            var appendixList = new List<DataModels.Appendix>();
            var chapterList = new List<DataModels.Chapter>();
            var sectionList = new List<DataModels.Section>();
            var ruleList = new List<DataModels.Rule>();
            var subRuleList = new List<DataModels.SubRule>();

            foreach (var rulebook in rulebooks)
            {
                rulebookList.Add(rulebook.ToDataModel());

                foreach (var glossary in rulebook.Glossaries) 
                {
                    glossaryList.Add(glossary.ToDataModel(rulebook.Id));
                }

                foreach (var appendix in rulebook.Appendices)
                {
                    appendixList.Add(appendix.ToDataModel(rulebook.Id));
                }

                foreach (var chapter in rulebook.Chapters)
                {
                    chapterList.Add(chapter.ToDataModel(rulebook.Id));

                    foreach (var section in chapter.Sections)
                    {
                        sectionList.Add(section.ToDataModel(chapter.Id));

                        foreach (var rule in section.Rules)
                        {
                            ruleList.Add(rule.ToDataModel(section.Id));

                            foreach (var subRule in rule.SubRules)
                            {
                                subRuleList.Add(subRule.ToDataModel(rule.Id));
                            }
                        }
                    }
                }
            }

            using (var con = new SQLiteConnection(_databaseName))
            {
                con.InsertAll(rulebookList);
                con.InsertAll(chapterList);
                con.InsertAll(glossaryList);
                con.InsertAll(appendixList);
                con.InsertAll(sectionList);
                con.InsertAll(ruleList);
                con.InsertAll(subRuleList);
            }
        }
    }
}