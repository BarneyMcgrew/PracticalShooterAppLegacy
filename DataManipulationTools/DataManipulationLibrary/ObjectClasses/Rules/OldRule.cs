using System;
using System.Collections.Generic;
using System.Text;

namespace DataManipulationLibrary.ObjectClasses.Rules
{
    public class OldRule
    {
        public OldRule()
        {

        }

        public Guid RuleId;
        public string Numeric;
        public string Name;
        public string Description;
        public Guid SectionId;
        public Guid DisciplineId;
        public string SearchableString;

        public List<OldRuleSpan> SpanList;
        public List<OldSubRule> SubRules;
    }
}
