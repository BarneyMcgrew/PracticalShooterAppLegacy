using PracticalShooter.Models;
using PracticalShooter.Views;
using PracticalShooterLibrary.StaticResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static PracticalShooterLibrary.GlobalResources.GlobalEnums;

namespace PracticalShooter.ViewModels
{   
    public class RulesViewModel : BaseViewModel
    {
        private string _selectedRulebook;

        private List<Chapter> _chapters;

        private Chapter.Section _selectedSection;

        private bool _bookmarkButtonVisible = false;

        private bool _searchSectionVisible = false;

        private bool _disciplineSectionVisible = false;

        private string _searchQuery;

        private bool _searchFlagVisible = false;

        private string _searchNameText;

        public Command SearchButtonTapped { get; }

        public Command DisciplineNameTapped { get; }

        public Command SelectedDisciplinePickerChanged { get; }

        public Command SearchBarTextChanged { get; }

        public Command SectionSelected { get; }

        public RulesViewModel()
        {
            _chapters = new List<Chapter>();

            SearchButtonTapped = new Command(() => SearchButtonTappedCommand());
            DisciplineNameTapped = new Command(() => DisciplineNameTappedCommand());
            SelectedDisciplinePickerChanged = new Command(async () => await SelectedDisciplinePickerChangedCommand());
            SearchBarTextChanged = new Command(async () => SearchBarTextChangedCommand());
            SectionSelected = new Command(async () => await SectionSelectedCommand());

            SearchQuery = "";

            LoadChapters();
        }

        public void OnAppearing()
        {
            var selectedDiscipline = GlobalSettings.Current.SelectedDiscipline;
            SelectedRulebook = selectedDiscipline;

            SearchBarTextChangedCommand();
        }

        public string SelectedRulebook
        {
            get => _selectedRulebook;
            set => SetProperty(ref _selectedRulebook, value);
        }

        public List<Chapter> Chapters
        {
            get => _chapters;
            set => SetProperty(ref _chapters, value);
        }

        public Chapter.Section SelectedSection
        {
            get => _selectedSection;
            set => SetProperty(ref _selectedSection, value);
        }

        public bool BookmarkButtonVisible
        {
            get => _bookmarkButtonVisible;
            set => SetProperty(ref _bookmarkButtonVisible, value);
        }

        public bool SearchSectionVisible
        {
            get => _searchSectionVisible;
            set => SetProperty(ref _searchSectionVisible, value);
        }

        public bool DisciplineSectionVisible
        {
            get => _disciplineSectionVisible;
            set => SetProperty(ref _disciplineSectionVisible, value);
        }

        public string SearchQuery
        {
            get => _searchQuery;
            set => SetProperty(ref _searchQuery, value);
        }

        public bool SearchFlagVisible
        {
            get => _searchFlagVisible;
            set => SetProperty(ref _searchFlagVisible, value);
        }

        public string SearchNameText
        {
            get => _searchNameText;
            set => SetProperty(ref _searchNameText, value);
        }

        private void SearchButtonTappedCommand()
        {
            SearchSectionVisible = !SearchSectionVisible;
            DisciplineSectionVisible = false;
        }

        private void DisciplineNameTappedCommand()
        {
            DisciplineSectionVisible = !DisciplineSectionVisible;
            SearchSectionVisible = false;
        }

        private async Task SelectedDisciplinePickerChangedCommand()
        {
            GlobalSettings.Current.SelectedDiscipline = SelectedRulebook;

            GlobalSettings.Current.UpdateSettings();

            OnAppearing();
        }

        private void SearchBarTextChangedCommand()
        {
            if (string.IsNullOrWhiteSpace(_searchQuery))
            {
                SearchFlagVisible = false;

                LoadChapters();
            }
            else
            {
                SearchFlagVisible = true;
                SearchNameText = $"Search Results: \"{_searchQuery}\"";

                var searchedSections = SearchSections(_searchQuery);

                var chapter = new Chapter();

                foreach (var section in searchedSections)
                {
                    chapter.Add(new Chapter.Section(section));
                }

                var chapters = new List<Chapter>();

                chapters.Add(chapter);

                Chapters = chapters;
            }
        }

        private async Task SectionSelectedCommand()
        {
            if (SelectedSection == null)
                return;

            if (SelectedSection.Name == "12.1 - Appendices")
            {
                var selectedDiscipline = GlobalSettings.Current.SelectedDiscipline;
                RulebookSession.Current.SearchQuery = SearchQuery;

                await Shell.Current.GoToAsync($"{nameof(AppendixPage)}");
            }
            else
            {
                var section = GlobalResourceCache.Current.GetSection(SelectedSection.SectionId);

                RulebookSession.Current.CurrentSection = section;
                RulebookSession.Current.SearchQuery = SearchQuery;

                await Shell.Current.GoToAsync($"{nameof(SectionsPage)}");
            }

            SelectedSection = null;
        }

        private void LoadChapters()
        {
            var discipline = GlobalResourceCache.Current.GetDiscipline();
            var viewChapters = new List<Chapter>();

            foreach (var chapter in discipline.Chapters)
            {
                viewChapters.Add(new Chapter(chapter));
            }

            Chapters = viewChapters;
        }

        public List<PracticalShooterLibrary.ObjectClasses.Rules.Section> SearchSections(string query)
        {
            var searchCriteria = query.ToLowerInvariant();

            var searchResults = LoadSections().Where(o => o.SearchableString.Contains(searchCriteria));

            return searchResults.ToList();
        }

        public List<PracticalShooterLibrary.ObjectClasses.Rules.Section> LoadSections()
        {
            return GlobalResourceCache.Current.DisciplinesDictionary.Values
                .First(o => Enum.GetName(typeof(DisciplineTypes), o.DisciplineType) == GlobalSettings.Current.SelectedDiscipline.Replace(" ", ""))
                .Chapters.SelectMany(o => o.Sections).ToList();
        }
    }
}
