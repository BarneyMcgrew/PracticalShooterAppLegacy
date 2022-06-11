using SQLite;

namespace PracticalShooterApp.DataModels
{
    public class DataModelBase
    {
        [PrimaryKey]
        public int Id { get; set; }
    }
}