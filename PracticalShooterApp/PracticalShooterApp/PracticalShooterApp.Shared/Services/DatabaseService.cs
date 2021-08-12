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

        public List<Glossary> SearchGlossaries(string searchCriteria, Discipline discipline, Language language)
        {
            return _dbConnection.Query<Glossary>($@"
                SELECT G.* FROM Glossary G
                LEFT OUTER JOIN Rulebook R on G.RulebookId = R.Id
                WHERE
                    R.Discipline = {(int)discipline} AND
                    R.Language = {(int)language} AND
                    (G.Key LIKE '%{searchCriteria}%' OR
                     G.Value LIKE '%{searchCriteria}%')
            ");
        }

        public List<Appendix> SearchAppendices(string searchCriteria, Discipline discipline, Language language)
        {
            return _dbConnection.Query<Appendix>($@"
                SELECT A.* FROM Appendix A
                LEFT OUTER JOIN Rulebook R on A.RulebookId = R.Id
                WHERE
                    R.Discipline = {(int)discipline} AND
                    R.Language = {(int)language} AND
                    (A.Name LIKE '%{searchCriteria}%' OR
                     A.Description LIKE '%{searchCriteria}%' OR
                     A.MetaDescription LIKE '%{searchCriteria}%')
            ");
        }

        public List<Rule> SearchRules(string searchCriteria, Discipline discipline, Language language)
        {
            var ruleList = _dbConnection.Query<Rule>($@"
                SELECT DISTINCT R.* FROM Rule R
                LEFT OUTER JOIN SubRule SR on R.Id = SR.RuleId
                LEFT OUTER JOIN Section S on R.SectionId = S.Id
                LEFT OUTER JOIN Chapter C on S.ChapterId = C.Id
                LEFT OUTER JOIN Rulebook R2 on R2.Id = C.RulebookId
                WHERE
                    R2.Discipline = {(int)discipline} AND
                    R2.Language = {(int)language} AND
                    (R.Numeric LIKE '%{searchCriteria}%' OR
                    R.Name LIKE '%{searchCriteria}%' OR
                    R.Description LIKE '%{searchCriteria}%' OR
                    SR.Numeric LIKE '%{searchCriteria}%' OR
                    SR.Name LIKE '%{searchCriteria}%' OR
                    SR.Description LIKE '%{searchCriteria}%' OR
                    S.Numeric LIKE '%{searchCriteria}%' OR
                    S.Name LIKE '%{searchCriteria}%' OR
                    S.Description LIKE '%{searchCriteria}%' OR
                    C.Numeric LIKE '%{searchCriteria}%' OR
                    C.Name LIKE '%{searchCriteria}%' OR
                    C.Description LIKE '%{searchCriteria}%')
            ");
            
            var subRuleList = GetSubRules(ruleList.Select(o => o.Id));
            
            foreach (var rule in ruleList)
            {
                rule.SubRuleList = subRuleList.SingleOrDefault(o => o.Key == rule.Id);
            }

            return ruleList;
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
