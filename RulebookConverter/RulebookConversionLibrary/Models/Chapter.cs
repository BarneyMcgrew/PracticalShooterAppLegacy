using System.Collections.Generic;
using RulebookConversionLibrary.Helpers;

namespace RulebookConversionLibrary.Models
{
    public class Chapter : RuleModelBase
    {
        public Chapter()
        {
            Id = IncrementIdHelper.GetNewId(GetType());
            Sections = new List<Section>();
        }

        public List<Section> Sections;

        public DataModels.Chapter ToDataModel(int rulebookId)
        {
            return new DataModels.Chapter()
            {
                Id = this.Id,
                Numeric = this.Numeric,
                Name = this.Name,
                Description = this.Description,
                RulebookId = rulebookId
            };
        }
    }
}