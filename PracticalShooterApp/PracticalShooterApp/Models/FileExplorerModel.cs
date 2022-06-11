using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PracticalShooterApp.Models
{
    [DataContract]
    public class FileExplorerModel
    {
        [DataMember(Name = "fileName")]
        [JsonProperty(PropertyName = "fileName")]
        public String FileName { get; set; }
        
        [DataMember(Name = "description")]
        [JsonProperty(PropertyName = "description")]
        public String Description { get; set; }
        
        [DataMember(Name = "fileDirectory")]
        [JsonProperty(PropertyName = "fileDirectory")]
        public String FileDirectory { get; set; }
    }
}