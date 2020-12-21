using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalShooter.Models
{
    public class Chapter : List<Section>
    {
        private string _name;
        private string _description;

        public Chapter() { }

        public Chapter(PracticalShooterLibrary.ObjectClasses.Rules.Chapter chapter)
        {
            Name = $"{chapter.Numeric} - {chapter.Name}";
            Description = chapter.Description;

            foreach (var section in chapter.Sections)
            {
                Add(new Section(section));
            }
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
