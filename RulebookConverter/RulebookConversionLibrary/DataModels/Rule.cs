using System;
using System.ComponentModel.DataAnnotations.Schema;
using RulebookConversionLibrary.Helpers;

namespace RulebookConversionLibrary.DataModels
{
    public class Rule : RuleModelBase
    {
        public int SectionId { get; set; }
    }
}