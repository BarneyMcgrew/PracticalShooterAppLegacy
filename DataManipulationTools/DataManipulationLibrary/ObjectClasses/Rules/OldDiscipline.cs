using System;
using System.Collections.Generic;
using static IPSCCompanionLibrary.GlobalResources.GlobalEnums;

namespace DataManipulationLibrary.ObjectClasses.Rules
{
    public class OldDiscipline
    {
        public OldDiscipline()
        {
            DisciplineId = Guid.NewGuid();
            Chapters = new List<OldChapter>();
            GlossaryList = new List<OldGlossary>();
        }

        [Obsolete]
        public string Language;
        [Obsolete]
        public string Name;

        public LanguageTypes LanguageType;
        public DisciplineTypes DisciplineType;

        public Guid DisciplineId;
        public List<OldGlossary> GlossaryList;
        public List<OldChapter> Chapters;
    }
}
