using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PracticalShooterApp.Models
{
    [DataContract]
    public class SubRuleModel
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
        
        [DataMember(Name = "RuleId")]
        [JsonProperty(PropertyName = "RuleId")]
        public int RuleId { get; set; }
    }
}