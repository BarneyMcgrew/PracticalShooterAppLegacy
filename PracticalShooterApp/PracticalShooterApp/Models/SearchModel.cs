using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PracticalShooterApp.Models
{
    [DataContract]
    public class SearchModel : RuleModel
    {
        [DataMember(Name = "SectionName")]
        [JsonProperty(PropertyName = "SectionName")]
        public string SectionName { get; set; }

        [DataMember(Name = "ChapterName")]
        [JsonProperty(PropertyName = "ChapterName")]
        public string ChapterName { get; set; }
        
        [DataMember(Name = "RulebookName")]
        [JsonProperty(PropertyName = "RulebookName")]
        public string RulebookName { get; set; }
    }
}