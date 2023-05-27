using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PracticalShooterApp.Models
{
    [DataContract]
    public class HomeTilesModel
    {
        [DataMember(Name = "itemTypeId")]
        [JsonProperty(PropertyName = "itemTypeId")]
        public String ItemTypeId { get; set; }
        
        [DataMember(Name = "itemId")]
        [JsonProperty(PropertyName = "itemId")]
        public String ItemId { get; set; }
        
        [DataMember(Name = "itemDescription")]
        [JsonProperty(PropertyName = "itemDescription")]
        public String ItemDescription { get; set; }
        
        [DataMember(Name = "itemImage")]
        [JsonProperty(PropertyName = "itemImage")]
        public String ItemImage { get; set; }
        
        [DataMember(Name = "itemName")]
        [JsonProperty(PropertyName = "itemName")]
        public String ItemName { get; set; }
        
        [DataMember(Name = "region")]
        [JsonProperty(PropertyName = "region")]
        public String Region { get; set; }

        [DataMember(Name = "itemOrder")]
        [JsonProperty(PropertyName = "itemOrder")]
        public Int32 ItemOrder { get; set; }
        
        [DataMember(Name = "navigationContext")]
        [JsonProperty(PropertyName = "navigationContext")]
        public String NavigationContext { get; set; }
        
        [DataMember(Name = "buildVersion")]
        [JsonProperty(PropertyName = "buildVersion")]
        public String BuildVersion { get; set; }
        
        [DataMember(Name = "useTileCard")]
        public bool UseTileCard => ItemTypeId == "TileCard";
        
        [DataMember(Name = "useListCard")]
        public bool UseListCard => ItemTypeId == "ListCard";

        [DataMember(Name = "useAdCard")] 
        public bool UseAdCard => ItemTypeId == "AdCard";
    }
}