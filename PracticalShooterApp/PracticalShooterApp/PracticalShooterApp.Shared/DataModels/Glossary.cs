using PracticalShooterApp.Shared.DataModels.BaseModels;

namespace PracticalShooterApp.Shared.DataModels
{
    public class Glossary : DataBaseModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
        
        public int RulebookId { get; set; }
    }
}