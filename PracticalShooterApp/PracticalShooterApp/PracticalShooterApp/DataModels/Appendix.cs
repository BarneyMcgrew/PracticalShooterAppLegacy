namespace PracticalShooterApp.DataModels
{
    public class Appendix : DataModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string MetaDescription { get; set; }

        public int RulebookId { get; set; }
    }
}