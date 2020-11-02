using System;
using System.Collections.Generic;
using System.Text;

namespace DataManipulationLibrary.ObjectClasses.Rules
{
    public class OldSubRule
    {
        public OldSubRule()
        {
            SubRuleId = Guid.NewGuid();
        }

        public Guid SubRuleId;
        public string Numeric;
        public string Name;
        public string Description;
        public Guid RuleId;
        public Guid DisciplineId;
        public string SearchableString;

        public List<OldRuleSpan> SpanList;
    }
}
