using PracticalShooterLibrary.ObjectClasses.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalShooterLibrary.StaticResources
{
    public class RulebookSession
    {
        private string _searchQuery;
        private Section _currentSection;

        public static RulebookSession Current = new RulebookSession();

        public RulebookSession()
        { }

        public string SearchQuery
        {
            get => _searchQuery;
            set => _searchQuery = value;
        }

        public Section CurrentSection
        {
            get => _currentSection;
            set => _currentSection = value;
        }
    }
}
