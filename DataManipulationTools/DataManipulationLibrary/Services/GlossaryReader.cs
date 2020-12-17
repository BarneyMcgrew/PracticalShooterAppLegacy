using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using DataManipulationLibrary.Helpers;
using PracticalShooterLibrary.ObjectClasses.Rules;

namespace DataManipulationLibrary.Services
{
    public class GlossaryReader
    {
        private readonly Regex _glossaryRegex = new Regex(@"(?<name>((\w|\(|\)|\/|-)*\s?)*)\.*(?<definition>.*)");

        public GlossaryReader() { }

        private List<Glossary> ConvertToGlossary(string[] fileArray)
        {
            var glossaries = new List<Glossary>();

            foreach (var line in fileArray)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var glossaryRegex = _glossaryRegex.Match(line);

                var glossary = new Glossary()
                {
                    Name = glossaryRegex.Groups["name"].Value,
                    Definition = glossaryRegex.Groups["definition"].Value
                };

                glossaries.Add(glossary);

            }

            return glossaries;
        }
    }
}
