using System;
using RulebookConversionLibrary.Helpers;

namespace RulebookConversionLibrary.Models
{
    public class Glossary
    {
        public Glossary()
        {
            Id = IncrementIdHelper.GetNewId(GetType());
        }

        public int Id;
        public string Key;
        public string Value;

        public DataModels.Glossary ToDataModel(int rulebookId)
        {
            return new DataModels.Glossary()
            {
                Id = this.Id,
                Key = this.Key,
                Value = this.Value,
                RulebookId = rulebookId
            };
        }
    }
}