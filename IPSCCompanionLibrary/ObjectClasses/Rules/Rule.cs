using System;
using System.Collections.Generic;
using System.Text;

namespace IPSCCompanionLibrary.ObjectClasses.Rules
{
    public class Rule
    {
        public Rule()
        {

        }

        public Guid RuleId;
        public string Numeric;
        public string Name;
        public string Description;
        public Guid SectionId;
        public Guid DisciplineId;
        public string SearchableString;

        public List<RuleSpan> SpanList;
        public List<SubRule> SubRules;
    }
}
