using IPSCCompanionLibrary.ObjectClasses.Rules;
using IPSCCompanionLibrary.StaticResources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IPSCCompanionLibrary.Services
{
    public class RulebookReader
    {
        public RulebookReader()
        { }

        public Rulebook ReadRulebookResource()
        {
            var file = UtilityHelper.ReadStreamFromFile("Rulebook");
            StreamReader reader = new StreamReader(file);
            string text = reader.ReadToEnd();
            var rulebook = JsonConvert.DeserializeObject<Rulebook>(text);

            foreach (var rulebookDiscipline in rulebook.Disciplines)
            {
                var glossarySection = rulebookDiscipline.Chapters.SelectMany(o => o.Sections)
                    .First(o => o.Name == "Glossary");

                var glossary = rulebookDiscipline.GlossaryList;

                foreach (var glossaryItem in glossary)
                {
                    glossarySection.Rules.Add(new Rule()
                    {
                        RuleId = glossaryItem.GlossaryId,
                        Name = glossaryItem.Name,
                        Description = glossaryItem.Definition
                    });
                }
            }

            return rulebook;
        }
    }
}
