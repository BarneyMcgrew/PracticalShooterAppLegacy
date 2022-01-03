using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PracticalShooterApp.Models
{
    [DataContract]
    public class LearnSectionsModel
    {
        [DataMember(Name = "sectionId")]
        [JsonProperty(PropertyName = "sectionId")]
        public int SectionId { get; set; }
        
        [DataMember(Name = "chapterGrouping")]
        [JsonProperty(PropertyName = "chapterGrouping")]
        public String ChapterGrouping { get; set; }
        
        [DataMember(Name = "sectionName")]
        [JsonProperty(PropertyName = "sectionName")]
        public String SectionName { get; set; }
    }
}