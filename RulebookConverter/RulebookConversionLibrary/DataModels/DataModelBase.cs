using System;
using SQLite;

namespace RulebookConversionLibrary.DataModels
{
    public class DataModelBase
    {
        [PrimaryKey]
        public int Id { get; set; }
    }
}