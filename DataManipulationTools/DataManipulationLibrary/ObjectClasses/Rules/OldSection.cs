using System;
using System.Collections.Generic;
using System.Text;

namespace DataManipulationLibrary.ObjectClasses.Rules
{
    public class OldSection
    {
        public OldSection()
        {
            SectionId = Guid.NewGuid();
            Rules = new List<OldRule>();
        }

        public Guid SectionId;
        public string Numeric;
        public string Name;
        public string Description;
        public Guid ChapterId;
        public Guid DisciplineId;
        public string SearchableString;

        public List<OldRule> Rules;
    }
}
