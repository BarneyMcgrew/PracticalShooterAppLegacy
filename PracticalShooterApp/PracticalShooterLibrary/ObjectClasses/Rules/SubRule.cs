using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalShooterLibrary.ObjectClasses.Rules
{
    public class SubRule
    {
        public SubRule()
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

        public List<RuleSpan> SpanList;
    }
}
