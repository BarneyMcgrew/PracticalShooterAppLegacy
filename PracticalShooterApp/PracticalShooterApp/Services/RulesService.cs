﻿using System.Collections.Generic;
using System.Threading.Tasks;
using PracticalShooterApp.Clients;
using PracticalShooterApp.DataModels;
using SQLite;
using Xamarin.Forms;

namespace PracticalShooterApp.Services
{
    public class RulesService : IRulesService
    {
        private SQLiteAsyncConnection _sqlClient => DependencyService.Get<ISqlRulesClient>().GetConnection();
        
        public RulesService()
        {}

        public async Task<List<Rule>> GetRulesBySectionId(int sectionId)
        {
            return await _sqlClient.Table<Rule>().Where(o => o.SectionId == sectionId).ToListAsync();
        }

        public async Task<Rule> GetRuleByRuleId(int ruleId)
        {
            return await _sqlClient.Table<Rule>().FirstAsync(o => o.Id == ruleId);
        }

        public async Task<List<SubRule>> GetSubRulesByRuleId(int ruleId)
        {
            return await _sqlClient.Table<SubRule>().Where(o => o.RuleId == ruleId).ToListAsync();
        }

        public async Task<List<Rule>> GetAllRules()
        {
            return await _sqlClient.Table<Rule>().ToListAsync();
        }

        public async Task<List<SubRule>> GetAllSubRules()
        {
            return await _sqlClient.Table<SubRule>().ToListAsync();
        }
    }
}