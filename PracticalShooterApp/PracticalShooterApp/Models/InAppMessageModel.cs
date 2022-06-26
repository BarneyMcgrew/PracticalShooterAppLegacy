using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PracticalShooterApp.Models
{
    [DataContract]
    public class InAppMessageModel
    {
        [DataMember(Name = "inAppMessageType")]
        [JsonProperty(PropertyName = "inAppMessageType")]
        public String InAppMessageType { get; set; }
        
        [DataMember(Name = "inAppMessageId")]
        [JsonProperty(PropertyName = "inAppMessageId")]
        public String InAppMessageId { get; set; }
        
        [DataMember(Name = "messageSubject")]
        [JsonProperty(PropertyName = "messageSubject")]
        public String MessageSubject { get; set; }
        
        [DataMember(Name = "messageDescription")]
        [JsonProperty(PropertyName = "messageDescription")]
        public String MessageDescription { get; set; }
        
        [DataMember(Name = "messageUrl")]
        [JsonProperty(PropertyName = "messageUrl")]
        public String MessageUrl { get; set; }
        
        [DataMember(Name = "expiryDate")]
        [JsonProperty(PropertyName = "expiryDate")]
        public DateTime ExpiryDate { get; set; } 
        
        [DataMember(Name = "isNew")]
        [JsonProperty(PropertyName = "isNew")]
        public bool IsNew { get; set; }
        
        [DataMember(Name = "isDeleted")]
        [JsonProperty(PropertyName = "isDeleted")]
        public bool IsDeleted { get; set; }
    }
}