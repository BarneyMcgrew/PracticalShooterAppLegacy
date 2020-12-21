using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
