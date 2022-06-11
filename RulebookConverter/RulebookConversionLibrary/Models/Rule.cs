using System;
using System.Collections.Generic;
using RulebookConversionLibrary.Helpers;

namespace RulebookConversionLibrary.Models
{
    public class Rule : RuleModelBase
    {
        public Rule() : base()
        {
            Id = IncrementIdHelper.GetNewId(GetType());
            SubRules = new List<SubRule>();
        }

        public List<SubRule> SubRules;

        public DataModels.Rule ToDataModel(int sectionId)
        {
            return new DataModels.Rule()
            {
                Id = this.Id,
                Numeric = this.Numeric,
                Name = this.Name,
                Description = this.Description,
                SectionId = sectionId
            };
        }
    }
}