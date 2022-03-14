using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Transactions;
using Newtonsoft.Json;

namespace PracticalShooterApp.Models
{
    [DataContract]
    public class RuleModel
    {
        [DataMember(Name = "Id")]
        [JsonProperty(PropertyName = "Id")]
        public int Id { get; set; }
        
        [DataMember(Name = "Name")]
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
        
        [DataMember(Name = "Numeric")]
        [JsonProperty(PropertyName = "Numeric")]
        public string Numeric { get; set; }
        
        [DataMember(Name = "Description")]
        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }
        
        [DataMember(Name = "SectionId")]
        [JsonProperty(PropertyName = "SectionId")]
        public int SectionId { get; set; }
        
        [DataMember(Name = "IsNotDescription")]
        [JsonProperty(PropertyName = "IsNotDescription")]
        public bool IsNotDescription { get; set; }
        
        [DataMember(Name = "IsNotSubRule")]
        [JsonProperty(PropertyName = "IsNotSubRule")]
        public bool IsNotSubRule { get; set; }
        
        [DataMember(Name = "SubRules")]
        [JsonProperty(PropertyName = "SubRules")]
        public List<SubRuleModel> SubRules { get; set; } 
    }
}