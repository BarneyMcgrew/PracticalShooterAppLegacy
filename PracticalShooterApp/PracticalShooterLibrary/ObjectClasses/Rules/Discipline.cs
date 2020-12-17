using System;
using System.Collections.Generic;
using static PracticalShooterLibrary.GlobalResources.GlobalEnums;

namespace PracticalShooterLibrary.ObjectClasses.Rules
{
    public class Discipline
    {
        public Discipline()
        {
            DisciplineId = Guid.NewGuid();
            Chapters = new List<Chapter>();
            GlossaryList = new List<Glossary>();
        }

        public LanguageTypes LanguageType;
        public DisciplineTypes DisciplineType;

        public Guid DisciplineId;
        public List<Glossary> GlossaryList;
        public List<Chapter> Chapters;
    }
}
