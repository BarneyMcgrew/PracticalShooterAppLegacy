using System.Collections.Generic;
using System.Threading.Tasks;
using PracticalShooterApp.DataModels;

namespace PracticalShooterApp.Services
{
    public interface IRulesService
    {
        Task<List<Rule>> GetRulesBySectionId(int sectionId);
        Task<Rule> GetRuleByRuleId(int ruleId);
        Task<List<SubRule>> GetSubRulesByRuleId(int ruleId);
        Task<List<Rule>> GetAllRules();
        Task<List<SubRule>> GetAllSubRules();
    }
}