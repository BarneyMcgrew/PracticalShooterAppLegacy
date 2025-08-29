using System;

namespace RulebookConversionLibrary.DataModels
{
    public class Revision
    {
        public int Id { get; set; }
        public string Code { get; set; } // e.g., "012019"
        public DateTime Date { get; set; } // e.g., 2019-01-01
    }
}

