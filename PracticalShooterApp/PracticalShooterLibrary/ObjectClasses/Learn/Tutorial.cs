using System;
using System.Collections.Generic;
using System.Text;
using static IPSCCompanionLibrary.GlobalResources.GlobalEnums;

namespace IPSCCompanionLibrary.ObjectClasses.Learn
{
    public class Tutorial
    {
        public Tutorial() 
        {
            TutorialId = Guid.NewGuid();
            Sections = new List<Section>();
        }

        public Guid TutorialId;
        public string TutorialName;
        public DisciplineTypes DisciplineType;
        public List<FilterTypes> FilterTypes;
        public List<Section> Sections;
    }
}
