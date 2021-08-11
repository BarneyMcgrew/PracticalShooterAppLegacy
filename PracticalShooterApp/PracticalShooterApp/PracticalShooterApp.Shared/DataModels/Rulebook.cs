using PracticalShooterApp.Shared.DataModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalShooterApp.Shared.DataModels
{
    public class Rulebook : DataBaseModel
    {
        public Enums.Discipline Discipline { get; set; }
        public Enums.Language Language { get; set; }
    }
}
