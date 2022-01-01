using System;
using System.ComponentModel.DataAnnotations.Schema;
using RulebookConversionLibrary.Helpers;

namespace RulebookConversionLibrary.DataModels
{
    public class SubRule : RuleModelBase
    {
        public int RuleId { get; set; }
    }
}