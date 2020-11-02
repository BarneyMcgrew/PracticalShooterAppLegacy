using System;
using System.Collections.Generic;
using System.Text;
using static IPSCCompanionLibrary.GlobalResources.GlobalEnums;

namespace DataManipulationLibrary.ObjectClasses.Rules
{
    public class OldRuleSpan
    {
        public OldRuleSpan() {}

        public string Text;
        public SpanTypes SpanType;
        public Guid ReferenceId;

        [Obsolete]
        public Guid? GlossaryId;
        [Obsolete]
        public Guid? RuleId;
        [Obsolete]
        public Guid? SubRuleId;
        [Obsolete]
        public Guid? SectionId;
    }
}
