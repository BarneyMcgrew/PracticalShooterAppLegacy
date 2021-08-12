using System.Collections.Generic;
using PracticalShooterApp.Shared.DataModels.BaseModels;

namespace PracticalShooterApp.Shared.DataModels
{
    public class Rule : RuleBaseModel
    {
        public int SectionId { get; set; }
        
        public IEnumerable<SubRule> SubRuleList { get; set; }
    }
}