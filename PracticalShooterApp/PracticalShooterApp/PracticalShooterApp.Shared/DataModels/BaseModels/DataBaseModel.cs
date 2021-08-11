using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalShooterApp.Shared.DataModels.BaseModels
{
    public class DataBaseModel
    {
        [PrimaryKey]
        public int Id { get; set; }
    }
}
