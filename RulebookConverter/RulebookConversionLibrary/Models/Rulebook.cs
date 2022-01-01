using System;
using System.Collections.Generic;
using RulebookConversionLibrary.Helpers;

namespace RulebookConversionLibrary.Models
{
    public class Rulebook
    {
        public Rulebook()
        {
            Id = IncrementIdHelper.GetNewId(GetType());
            Chapters = new List<Chapter>();
            Appendices = new List<Appendix>();
            Glossaries = new List<Glossary>();
            Indices = new List<Index>();
        }

        public int Id;
        public Enums.Discipline Discipline;
        public Enums.Language Language;

        public List<Chapter> Chapters;
        public List<Appendix> Appendices;
        public List<Glossary> Glossaries;
        public List<Index> Indices;

        public DataModels.Rulebook ToDataModel()
        {
            return new DataModels.Rulebook()
            {
                Id = this.Id,
                Language = this.Language,
                Discipline = this.Discipline
            };
        }
    }
}