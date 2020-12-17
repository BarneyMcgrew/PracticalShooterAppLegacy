using System;
using System.Collections.Generic;

namespace PracticalShooterLibrary.ObjectClasses.Rules
{
    public class Chapter
    {
        public Chapter() 
        {
            ChapterId = Guid.NewGuid();

        }

        public Guid ChapterId;
        public Guid DisciplineId;
        public string Numeric;
        public string Name;
        public string Description;

        public List<Section> Sections;

        
    }
}
