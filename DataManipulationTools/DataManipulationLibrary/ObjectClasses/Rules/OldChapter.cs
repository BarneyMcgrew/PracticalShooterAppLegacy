using System;
using System.Collections.Generic;

namespace DataManipulationLibrary.ObjectClasses.Rules
{
    public class OldChapter
    {
        public OldChapter() 
        {
            ChapterId = Guid.NewGuid();

        }

        public Guid ChapterId;
        public Guid DisciplineId;
        public string Numeric;
        public string Name;
        public string Description;

        public List<OldSection> Sections;

        
    }
}
