using System;
using RulebookConversionLibrary.Helpers;

namespace RulebookConversionLibrary.Models
{
    public class Appendix
    {
        public Appendix()
        {
            Id = IncrementIdHelper.GetNewId(GetType());
        }

        public int Id;
        public string Name;
        public string Description;
        public string MetaDesciption;

        public DataModels.Appendix ToDataModel(int rulebookId)
        {
            return new DataModels.Appendix()
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                MetaDescription = this.MetaDesciption,
                RulebookId = rulebookId
            };
        }
    }
}