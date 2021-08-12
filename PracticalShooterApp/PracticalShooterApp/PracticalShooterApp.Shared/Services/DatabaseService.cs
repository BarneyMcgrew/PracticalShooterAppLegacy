using PracticalShooterApp.Shared.DataModels;
using PracticalShooterApp.Shared.Interfaces.Repositories;
using PracticalShooterApp.Shared.Interfaces.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PracticalShooterApp.Shared.Enums;
using Xamarin.Forms;

namespace PracticalShooterApp.Shared.Services
{
    public class DatabaseService : IDatabaseService
    {
        SQLiteConnection _dbConnection;

        public DatabaseService()
        {
            _dbConnection = DependencyService.Get<IDatabaseRepository>().CreateConnection();
        }

        public List<Glossary> GetGlossaries(Discipline discipline, Language language)
        {
            return _dbConnection.Query<Glossary>($@"
                SELECT
                       G.*
                FROM
                     Rulebook R
                INNER JOIN
                         Glossary G on R.Id = G.RulebookId
                WHERE
                  R.Discipline = {(int)discipline}
                AND
                  R.Language = {(int)language}
            ");
        }

        public List<Appendix> GetAppendices(Discipline discipline, Language language)
        {
            return _dbConnection.Query<Appendix>($@"
                SELECT
                       A.*
                FROM
                     Rulebook R
                INNER JOIN
                         Appendix A on R.Id = A.RulebookId
                WHERE
                  R.Discipline = {(int)discipline}
                AND
                  R.Language = {(int)language}
            ");
        }

        public List<Chapter> GetChapters(Discipline discipline, Language language)
        {
            return _dbConnection.Query<Chapter>($@"
                SELECT
                       C.*
                FROM
                     Rulebook R
                INNER JOIN
                         Chapter C on R.Id = C.RulebookId
                WHERE
                  R.Discipline = {(int)discipline}
                AND
                  R.Language = {(int)language}
            ");
        }

        public List<Section> GetSections(int chapterId)
        {
            return _dbConnection.Query<Section>($@"
                SELECT 
                       * 
                FROM 
                     Section
                WHERE 
                      ChapterId = {chapterId}
            ");
        }

        public List<Rule> GetRules(int sectionId)
        {
            var ruleList = _dbConnection.Query<Rule>($@"
                SELECT
                       *
                FROM
                     Rule
                WHERE
                      SectionId = {sectionId}
            ");

            var subRuleList = GetSubRules(ruleList.Select(o => o.Id));
            
            foreach (var rule in ruleList)
            {
                rule.SubRuleList = subRuleList.SingleOrDefault(o => o.Key == rule.Id);
            }

            return ruleList;
        }

        private IEnumerable<IGrouping<int, SubRule>> GetSubRules(IEnumerable<int> ruleIds)
        {
            return _dbConnection.Query<SubRule>($@"
                SELECT
                       *
                FROM
                     SubRule
                WHERE
                      RuleId IN ({String.Join(", ", ruleIds)})
            ").GroupBy(o => o.RuleId);
        }
    }
}
