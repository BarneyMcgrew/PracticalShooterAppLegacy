namespace PracticalShooterApp.DataModels
{
    public class Glossary : DataModelBase
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public int RulebookId { get; set; }
    }
}