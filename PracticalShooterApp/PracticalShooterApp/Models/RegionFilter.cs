using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PracticalShooterApp.Models
{
    [DataContract]
    public class RegionFilter
    {
        [DataMember(Name = "regionName")]
        [JsonProperty(PropertyName = "regionName")]
        public String RegionName { get; set; }
        
        [DataMember(Name = "regionSelected")]
        [JsonProperty(PropertyName = "regionSelected")]
        public bool RegionSelected { get; set; }
    }
}