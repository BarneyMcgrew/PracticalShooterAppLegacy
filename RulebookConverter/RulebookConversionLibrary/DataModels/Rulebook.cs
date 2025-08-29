using System;
using RulebookConversionLibrary.Helpers;
using SQLite;

namespace RulebookConversionLibrary.DataModels
{
    public class Rulebook : DataModelBase
    {
        public int DisciplineId { get; set; }
        public int LanguageId { get; set; }
        public int RevisionId { get; set; }
    }
}