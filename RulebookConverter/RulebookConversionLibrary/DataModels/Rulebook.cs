using System;
using RulebookConversionLibrary.Helpers;
using SQLite;

namespace RulebookConversionLibrary.DataModels
{
    public class Rulebook : DataModelBase
    {
        public Enums.Discipline Discipline { get; set; }
        public Enums.Language Language { get; set; }
    }
}