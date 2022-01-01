using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RulebookConversionLibrary.DataModels
{
    public class Chapter : RuleModelBase
    {
        public int RulebookId { get; set; }
    }
}