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
        private string _currentPDFFileName;
        private string _currentPDFFriendlyName;

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

        public string CurrentPDFFileName
        {
            get => _currentPDFFileName;
            set => _currentPDFFileName = value;
        }

        public string CurrentPDFFriendlyName
        {
            get => _currentPDFFriendlyName;
            set => _currentPDFFriendlyName = value;
        }
    }
}
