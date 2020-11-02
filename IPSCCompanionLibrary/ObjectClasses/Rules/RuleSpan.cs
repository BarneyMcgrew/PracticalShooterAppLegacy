using System;
using System.Collections.Generic;
using System.Text;
using static IPSCCompanionLibrary.GlobalResources.GlobalEnums;

namespace IPSCCompanionLibrary.ObjectClasses.Rules
{
    public class RuleSpan
    {
        public RuleSpan() {}

        public string Text;
        public SpanTypes SpanType;
        public Guid ReferenceId;
        public string ReferenceText;
    }
}
