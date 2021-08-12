using PracticalShooterApp.Shared.DataModels.BaseModels;

namespace PracticalShooterApp.Shared.DataModels
{
    public class Appendix : DataBaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string MetaDescription { get; set; }
        
        public int RulebookId { get; set; }
    }
}