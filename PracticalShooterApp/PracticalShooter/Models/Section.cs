using System;
using System.Collections.Generic;
using System.Text;
using static PracticalShooter.Models.Section.Rule;
using static PracticalShooterLibrary.GlobalResources.GlobalEnums;

namespace PracticalShooter.Models
{
    public class Section
    {
        private Guid _sectionId;
        private string _name;
        private string _description;
        
        public Section(PracticalShooterLibrary.ObjectClasses.Rules.Section section)
        {
            SectionId = section.SectionId;
            Name = $"{section.Numeric} - {section.Name}";
            Description = section.Description;
        }

        public Guid SectionId
        {
            get => _sectionId;
            set => _sectionId = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }

        public class Rule : List<SubRule>
        {
            private string _name;
            private string _description;
            private List<PracticalShooterLibrary.ObjectClasses.Rules.RuleSpan> _spanList;

            public Rule() { }

            public Rule(PracticalShooterLibrary.ObjectClasses.Rules.Rule rule)
            {
                if (string.IsNullOrWhiteSpace(rule.Numeric))
                {
                    Name = rule.Name;
                }
                else
                {
                    Name = $"{rule.Numeric} {rule.Name}";
                }

                Description = rule.Description.Replace("\\r\\n", Environment.NewLine + Environment.NewLine);
                SpanList = rule.SpanList;

                if (rule.SubRules != null)
                {
                    foreach (var subRule in rule.SubRules)
                    {
                        Add(new SubRule(subRule));
                    }
                }
            }

            public bool Visible => !string.IsNullOrWhiteSpace(Name);

            public string Name
            {
                get => _name;
                set => _name = value;
            }

            public string Description
            {
                get => _description;
                set => _description = value;
            }

            public List<PracticalShooterLibrary.ObjectClasses.Rules.RuleSpan> SpanList
            {
                get => _spanList;
                set => _spanList = value;
            }

            public class SubRule
            {
                private string _numeric;
                private string _name;
                private string _description;
                private List<PracticalShooterLibrary.ObjectClasses.Rules.RuleSpan> _spanList;

                public SubRule(PracticalShooterLibrary.ObjectClasses.Rules.SubRule subRule)
                {
                    Name = $"{subRule.Numeric} {subRule.Name}";
                    Description = subRule.Description.Replace("\\r\\n", Environment.NewLine + Environment.NewLine);
                    SpanList = subRule.SpanList;
                }

                public string Numeric
                {
                    get => _numeric;
                    set => _numeric = value;
                }

                public string Name
                {
                    get => _name;
                    set => _name = value;
                }

                public string Description
                {
                    get => _description;
                    set => _description = value;
                }

                public List<PracticalShooterLibrary.ObjectClasses.Rules.RuleSpan> SpanList
                {
                    get => _spanList;
                    set => _spanList = value;
                }
            }
        }

        public class RuleSpan
        {
            private string _text;
            private SpanTypes _spanType;
            private Guid _referenceId;
            private string _referenceText;

            public RuleSpan() { }

            public string Text
            {
                get => _text;
                set => _text = value;
            }

            public SpanTypes SpanType
            {
                get => _spanType;
                set => _spanType = value;
            }

            public Guid ReferenceId
            {
                get => _referenceId;
                set => _referenceId = value;
            }

            public string ReferenceText
            {
                get => _referenceText;
                set => _referenceText = value;
            }
        }
    }
}
