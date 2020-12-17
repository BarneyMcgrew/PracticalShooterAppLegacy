using System;
using System.Collections.Generic;
using System.Text;
using static IPSCCompanionLibrary.GlobalResources.GlobalEnums;

namespace IPSCCompanionLibrary.ObjectClasses.Learn
{
    public class TextSpan
    {
        public TextSpan() { }

        public string Text;
        public SpanTypes SpanType;
        public Guid ReferenceId;
    }
}
