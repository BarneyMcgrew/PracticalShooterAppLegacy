using System;
using System.Collections.Generic;
using RulebookConversionLibrary.Helpers;

namespace RulebookConversionLibrary.Models
{
    public class Section : RuleModelBase
    {
        public Section()
        {
            Id = IncrementIdHelper.GetNewId(GetType());
            Rules = new List<Rule>();
        }

        public List<Rule> Rules;

        public DataModels.Section ToDataModel(int chapterId)
        {
            return new DataModels.Section()
            {
                Id = this.Id,
                Numeric = this.Numeric,
                Name = this.Name,
                Description = this.Description,
                ChapterId = chapterId
            };
        }
    }
}