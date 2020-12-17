using FormsControls.Base;
using IPSCCompanionApp.UserInterfaces.RULEBOOKS;
using IPSCCompanionApp.UserInterfaces.UTILITIES;
using IPSCCompanionLibrary.StaticResources;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static IPSCCompanionLibrary.GlobalResources.GlobalEnums;

namespace IPSCCompanionApp.UserInterfaces.RULEBOOKS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RulebooksPage : ContentPage, IAnimationPage
    {
        public IPageAnimation PageAnimation { get; } = new PushPageAnimation() { Duration = AnimationDuration.Long, Subtype = AnimationSubtype.FromBottom };

        private string _searchQuery;

        public void OnAnimationStarted(bool isPopAnimation)
        {
            // Put your code here but leaving empty works just fine
        }

        public void OnAnimationFinished(bool isPopAnimation)
        {
            // Put your code here but leaving empty works just fine
        }

        public RulebooksPage()
        {
            InitializeComponent();            

            HeaderTab.GestureRecognizers.Add(new TapGestureRecognizer((view) => GoBackClicked()));
            DisciplineName.GestureRecognizers.Add(new TapGestureRecognizer((view) => DisciplineNameClicked()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var selectedDiscipline = GlobalSettings.Current.SelectedDiscipline;
            DisciplineName.Text = selectedDiscipline;
            SelectedDisciplinePicker.SelectedItem = selectedDiscipline;

            SearchBarOnly_TextChanged(null, null);
        }

        private void DisciplineNameClicked()
        {
            SelectedDisciplinePicker.Focus();
        }

        private void GoBackClicked()
        {
            Navigation.PopModalAsync();
        }

        private void DisciplineButton_Clicked(object sender, System.EventArgs e)
        {
            DisciplineSection.IsVisible = !DisciplineSection.IsVisible;
            SearchSection.IsVisible = false;
        }

        private void SearchButton_Clicked(object sender, System.EventArgs e)
        {
            SearchSection.IsVisible = !SearchSection.IsVisible;
            DisciplineSection.IsVisible = false;
        }

        private void SearchBarOnly_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e != null)
            {
                _searchQuery = e.NewTextValue;
            }


            if (string.IsNullOrWhiteSpace(_searchQuery))
            {
                SearchFlag.IsVisible = false;

                LoadChapters();
            }
            else
            {
                SearchFlag.IsVisible = true;
                SearchName.Text = $"Search Results: \"{_searchQuery}\"";

                var searchedSections = SearchSections(_searchQuery);

                var chapter = new Chapter();

                foreach (var section in searchedSections)
                {
                    chapter.Add(new Section(section));
                }

                var chapters = new List<Chapter>();

                chapters.Add(chapter);

                searchResults.ItemsSource = chapters;
            }
        }

        private void LoadChapters()
        {
            var discipline = GlobalResourceCache.Current.GetDiscipline();
            var viewChapters = new List<Chapter>();

            foreach (var chapter in discipline.Chapters)
            {
                viewChapters.Add(new Chapter(chapter));
            }

            searchResults.ItemsSource = viewChapters;
        }

        public List<IPSCCompanionLibrary.ObjectClasses.Rules.Section> LoadSections()
        {
            return GlobalResourceCache.Current.DisciplinesDictionary.Values
                .First(o => Enum.GetName(typeof(DisciplineTypes), o.DisciplineType) == GlobalSettings.Current.SelectedDiscipline.Replace(" ", ""))
                .Chapters.SelectMany(o => o.Sections).ToList();
        }

        public List<IPSCCompanionLibrary.ObjectClasses.Rules.Section> SearchSections(string query)
        {
            var searchCriteria = query.ToLowerInvariant();

            var searchResults = LoadSections().Where(o => o.SearchableString.Contains(searchCriteria));

            return searchResults.ToList();
        }

        private void SectionSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var section = (e.SelectedItem as Section);

            if (section.Name == "12.1 - Appendices")
            {
                var selectedDiscipline = GlobalSettings.Current.SelectedDiscipline;

                Navigation.PushModalAsync(new PDFPage($"{selectedDiscipline}-Appendices", "Appendices"));
            }                
            else
            {
                var sectionFull = GlobalResourceCache.Current.GetSection(section.SectionId);
                Navigation.PushModalAsync(new SectionsPage(sectionFull, _searchQuery));
            }

            var listView = sender as Xamarin.Forms.ListView;

            listView.SelectedItem = null;
        }

        private void SelectedDisciplinePicker_OnSelectedIndexChanged(object sender, System.EventArgs e)
        {
            var selectedDiscipline = (string)SelectedDisciplinePicker.SelectedItem;
            DisciplineName.Text = selectedDiscipline;
            GlobalSettings.Current.SelectedDiscipline = selectedDiscipline;

            GlobalSettings.Current.UpdateSettings();

            OnAppearing();
        }

        public class Section
        {
            public Section(IPSCCompanionLibrary.ObjectClasses.Rules.Section section)
            {
                SectionId = section.SectionId;
                Name = $"{section.Numeric} - {section.Name}";
                Description = section.Description;
            }

            public Guid SectionId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public class Chapter : List<Section>
        {
            public Chapter() { }

            public Chapter(IPSCCompanionLibrary.ObjectClasses.Rules.Chapter chapter)
            {
                Name = $"{chapter.Numeric} - {chapter.Name}";
                Description = chapter.Description;

                foreach (var section in chapter.Sections)
                {
                    Add(new Section(section));
                }
            }

            public string Name { get; set; }
            public string Description { get; set; }
        }
    }
}