using System.Runtime.Serialization;
using Xamarin.Forms.Internals;

namespace PracticalShooterApp.Models
{
    [DataContract]
    public class NavigationModel
    {
        #region Properties

        [DataMember(Name = "itemTypeId")]
        public string ItemTypeId { get; set; }

        [DataMember(Name = "itemId")]
        public string ItemId { get; set; }           

        [DataMember(Name = "itemName")]
        public string ItemName { get; set; }

        [DataMember(Name = "itemDescription")]
        public string ItemDescription { get; set; }

        [DataMember(Name = "itemImage")]
        public string ItemImage { get; set; }

        [DataMember(Name = "navigationContext")]
        public string NavigationContext { get; set; }

        [DataMember(Name = "itemOrder")]
        public int ItemOrder { get; set; }

        [DataMember(Name = "useTileCard")]
        public bool UseTileCard { get; set; }

        [DataMember(Name = "useListCard")]
        public bool UseListCard { get { return !UseTileCard; } }               

        #endregion
    }
}
