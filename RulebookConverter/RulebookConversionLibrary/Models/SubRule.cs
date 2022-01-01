using System;
using RulebookConversionLibrary.Helpers;

namespace RulebookConversionLibrary.Models
{
    public class SubRule : RuleModelBase
    {
        public SubRule()
        {
            Id = IncrementIdHelper.GetNewId(GetType());
        }

        public DataModels.SubRule ToDataModel(int ruleId)
        {
            return new DataModels.SubRule()
            {
                Id = this.Id,
                Numeric = this.Numeric,
                Name = this.Name,
                Description = this.Description,
                RuleId = ruleId
            };
        }
    }
}