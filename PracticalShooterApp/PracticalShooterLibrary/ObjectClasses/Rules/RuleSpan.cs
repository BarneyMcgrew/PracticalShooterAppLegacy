using System;
using System.Collections.Generic;
using System.Text;
using static PracticalShooterLibrary.GlobalResources.GlobalEnums;

namespace PracticalShooterLibrary.ObjectClasses.Rules
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
