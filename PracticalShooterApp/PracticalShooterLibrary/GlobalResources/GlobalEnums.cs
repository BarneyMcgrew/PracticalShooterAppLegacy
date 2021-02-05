using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalShooterLibrary.GlobalResources
{
    public class GlobalEnums
    {
        public enum AppVersions
        {
            Competitor = 1,
            RangeOfficer = 2,
            RangeMaster = 3
        }

        public enum OSVersions
        {
            Android = 1,
            iOS = 2
        }

        public enum SpanTypes
        {
            Normal,
            GlossaryTooltip,
            RuleReference,
            SubRuleReference,
            SectionReference,
            ExternalWebLink,
            InternalPDFLink,
            TutorialReference,
            DrillReference
        }

        public enum FilterTypes
        {
            Beginner,
            Intermediate,
            Advanced,
            Divisions,
            Rules,
            ActionAir,
            Handgun,
            MiniRifle,
            Rifle,
            Shotgun,
            PCC
        }

        public enum DisciplineTypes
        {
            ACTIONAIR = 0,
            HANDGUN = 1,
            SHOTGUN= 2,
            RIFLE = 3,
            MINIRIFLE = 4,
            PCC = 5
        }

        public enum LanguageTypes
        {
            English
        }
    }
}
