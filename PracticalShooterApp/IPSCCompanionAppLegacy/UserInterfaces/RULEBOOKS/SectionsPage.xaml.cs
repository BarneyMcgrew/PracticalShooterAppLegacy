using IPSCCompanionApp.UserInterfaces.UTILITIES;
using IPSCCompanionLibrary.GlobalResources;
using IPSCCompanionLibrary.ObjectClasses.Rules;
using IPSCCompanionLibrary.StaticResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IPSCCompanionApp.UserInterfaces.RULEBOOKS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SectionsPage : ContentPage
    {
        private IPSCCompanionLibrary.ObjectClasses.Rules.Section _section;
        private string _searchQuery;
        private string _sectionDescription;
        private IPSCCompanionLibrary.ObjectClasses.Rules.Rule rule;

        public SectionsPage()
        {
            InitializeComponent();

            InitialisePage();
        }

        public SectionsPage(IPSCCompanionLibrary.ObjectClasses.Rules.Section section, string searchQuery)
        {
            InitializeComponent();

            this._section = section;
            this._searchQuery = searchQuery;

            InitialisePage();
        }

        public SectionsPage(IPSCCompanionLibrary.ObjectClasses.Rules.Rule rule)
        {
            this.rule = rule;

            InitialisePage();
        }

        private void InitialisePage()
        {
            var selectedDiscipline = GlobalSettings.Current.SelectedDiscipline;
            DisciplineName.Text = selectedDiscipline;
            SelectedDisciplinePicker.SelectedItem = selectedDiscipline;

            HeaderTab.Text = _section.Numeric;
            SectionName.Text = _section.Name;
            _sectionDescription = _section.Description;
            SearchBarOnly.Text = _searchQuery;
            HeaderTab.GestureRecognizers.Add(new TapGestureRecognizer((view) => GoBackClicked()));
            DisciplineName.GestureRecognizers.Add(new TapGestureRecognizer((view) => DisciplineNameClicked()));
            PopUpGrid.GestureRecognizers.Add(new TapGestureRecognizer((view) => ChangePopUpVisibility(false)));

            SearchRules();

            LoadingDisplay.IsVisible = false;
        }

        private void DisciplineNameClicked()
        {
            SelectedDisciplinePicker.Focus();
        }

        private void ChangePopUpVisibility(bool visible)
        {
            PopUpGrid.IsVisible = visible;
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
            _searchQuery = e.NewTextValue;

            SearchRules();
        }

        private void SearchRules()
        {
            if (string.IsNullOrWhiteSpace(_searchQuery))
            {
                SearchFlag.IsVisible = false;
                SectionFlag.IsVisible = true;

                LoadRules();
            }
            else
            {
                SearchFlag.IsVisible = true;
                SectionFlag.IsVisible = false;
                SearchName.Text = $"Search Results: \"{_searchQuery}\"";

                var searchedRules = SearchRules(_section, _searchQuery);
                var viewRules = new List<Rule>();

                foreach (var rule in searchedRules)
                {
                    viewRules.Add(new Rule(rule));
                }

                PopulateTableView(viewRules, true);
            }
        }

        private void SelectedDisciplinePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedDiscipline = (string)SelectedDisciplinePicker.SelectedItem;
            DisciplineName.Text = selectedDiscipline;
            GlobalSettings.Current.SelectedDiscipline = selectedDiscipline;

            GlobalSettings.Current.UpdateSettings();

            ReloadSection();
        }

        private void ReloadSection()
        {
            _section = GlobalResourceCache.Current.GetSectionByNumeric(_section.Numeric);

            SectionName.Text = _section.Name;
            _sectionDescription = _section.Description;

            if (_section == null)
            {
                GoBackClicked();
            }
            else
            {
                SearchRules();
            }            
        }

        private List<IPSCCompanionLibrary.ObjectClasses.Rules.Rule> SearchRules(IPSCCompanionLibrary.ObjectClasses.Rules.Section section, string searchQuery)
        {
            var searchCriteria = searchQuery.ToLowerInvariant();

            var searchableRules = section.Rules.Where(o => o.SearchableString != null);
            
            var searchResults = searchableRules
                .Where(o => o.SearchableString.Contains(searchCriteria));

            return searchResults.ToList();
        }

        private void LoadRules()
        {
            var rules = _section.Rules;

            var viewRules = new List<Rule>();

            foreach (var rule in rules)
            {
                viewRules.Add(new Rule(rule));
            }

            PopulateTableView(viewRules, false);
        }

        private void PopulateTableView(List<Rule> rules, bool isSearch)
        {
            SearchResults.Clear();

            if (!string.IsNullOrWhiteSpace(_sectionDescription) && !isSearch)
            {
                SearchResults.Add(CreateDescriptionCell(_sectionDescription));
            }

            foreach (var rule in rules)
            {
                if (rule.SpanList == null)
                    continue;

                SearchResults.Add(CreateRuleViewCell(rule));

                foreach (var subRule in rule)
                {
                    SearchResults.Add(CreateSubRuleViewCell(subRule));
                }
            }
        }

        private ViewCell CreateDescriptionCell(string description)
        {
            var viewCell = new ViewCell();
            var stackLayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(10)
            };

            var label = new Label()
            {
                Text = description
            };


            stackLayout.Children.Add(label);
            viewCell.View = stackLayout;

            return viewCell;
        }

        private ViewCell CreateSubRuleViewCell(SubRule rule)
        {
            var viewCell = new ViewCell();
            var stackLayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(10, 15, 10, 15)
            };

            var headingLabel = new Label()
            {
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                Text = rule.Name
            };

            var ruleLabel = new Label()
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            };

            ruleLabel.FormattedText = ConvertSpans(rule.SpanList);

            stackLayout.Children.Add(headingLabel);
            stackLayout.Children.Add(ruleLabel);

            viewCell.View = stackLayout;

            return viewCell;
        }

        private ViewCell CreateRuleViewCell(Rule rule)
        {
            var viewCell = new ViewCell();

            var stackLayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(10, 15, 10, 15)
            };

            var headingLabel = new Label()
            {
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                Text = rule.Name
            };

            var ruleLabel = new Label()
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            };

            ruleLabel.FormattedText = ConvertSpans(rule.SpanList);

            stackLayout.Children.Add(headingLabel);
            stackLayout.Children.Add(ruleLabel);

            viewCell.View = stackLayout;

            return viewCell;
        }

        private FormattedString ConvertSpans(List<RuleSpan> spanList)
        {
            var formattedString = new FormattedString();

            foreach (var ruleSpan in spanList)
            {
                switch (ruleSpan.SpanType)
                {
                    case GlobalEnums.SpanTypes.Normal:
                        {
                            formattedString.Spans.Add(new Span()
                            {
                                Text = ruleSpan.Text.Replace("\\r\\n", Environment.NewLine + Environment.NewLine)
                            });
                            break;
                        }
                    case GlobalEnums.SpanTypes.GlossaryTooltip:
                        {
                            if (GlobalSettings.Current.EnableGlossaryTooltips)
                            {
                                var referenceId = ruleSpan.ReferenceId;

                                var glossary = GetGlossary(referenceId);

                                formattedString.Spans.Add(new Span()
                                {
                                    Text = ruleSpan.Text,
                                    TextDecorations = TextDecorations.Underline,
                                    GestureRecognizers = { new TapGestureRecognizer((view) => ShowGlossaryTerm(glossary)) }
                                });
                            }
                            else
                            {
                                formattedString.Spans.Add(new Span()
                                {
                                    Text = ruleSpan.Text.Replace("\\r\\n", Environment.NewLine + Environment.NewLine)
                                });
                            }

                            break;
                        }
                    case GlobalEnums.SpanTypes.SectionReference:
                        {
                            if (GlobalSettings.Current.EnableRuleReferences)
                            {
                                var referenceId = ruleSpan.ReferenceId;

                                var section = GetSection(referenceId);

                                formattedString.Spans.Add(new Span()
                                {
                                    Text = ruleSpan.Text,
                                    TextColor = Color.Gold,
                                    GestureRecognizers = { new TapGestureRecognizer((view) => GoToSection(section)) }
                                });
                            }
                            else
                            {
                                formattedString.Spans.Add(new Span()
                                {
                                    Text = ruleSpan.Text.Replace("\\r\\n", Environment.NewLine + Environment.NewLine)
                                });
                            }

                            break;
                        }
                    case GlobalEnums.SpanTypes.RuleReference:
                        {
                            if (GlobalSettings.Current.EnableRuleReferences)
                            {
                                var referenceId = ruleSpan.ReferenceId;

                                var rule = GetRule(referenceId);

                                formattedString.Spans.Add(new Span()
                                {
                                    Text = ruleSpan.Text,
                                    TextColor = Color.Gold,
                                    GestureRecognizers = { new TapGestureRecognizer((view) => ShowRuleReference(rule)) }
                                });
                            }
                            else
                            {
                                formattedString.Spans.Add(new Span()
                                {
                                    Text = ruleSpan.Text.Replace("\\r\\n", Environment.NewLine + Environment.NewLine)
                                });
                            }

                            break;
                        }
                    case GlobalEnums.SpanTypes.SubRuleReference:
                        {
                            if (GlobalSettings.Current.EnableRuleReferences)
                            {
                                var referenceId = ruleSpan.ReferenceId;

                                var subRule = GetSubRule(referenceId);

                                formattedString.Spans.Add(new Span()
                                {
                                    Text = ruleSpan.Text,
                                    TextColor = Color.Gold,
                                    GestureRecognizers = { new TapGestureRecognizer((view) => ShowSubRuleReference(subRule)) }
                                });
                            }
                            else
                            {
                                formattedString.Spans.Add(new Span()
                                {
                                    Text = ruleSpan.Text.Replace("\\r\\n", Environment.NewLine + Environment.NewLine)
                                });
                            }

                            break;
                        }
                    case GlobalEnums.SpanTypes.InternalPDFLink:
                        {
                            var referenceText = ruleSpan.ReferenceText;

                            formattedString.Spans.Add(new Span()
                            {
                                Text = ruleSpan.Text,
                                TextColor = Color.Gold,
                                GestureRecognizers = { new TapGestureRecognizer((view) => ShowPdfReference(referenceText, ruleSpan.Text.ToUpper())) }
                            });

                            break;
                        }
                }
            }

            return formattedString;
        }

        private IPSCCompanionLibrary.ObjectClasses.Rules.SubRule GetSubRule(Guid referenceId)
        {
            return GlobalResourceCache.Current.GetSubRule(referenceId);
        }

        private IPSCCompanionLibrary.ObjectClasses.Rules.Rule GetRule(Guid referenceId)
        {
            return GlobalResourceCache.Current.GetRule(referenceId);
        }

        private IPSCCompanionLibrary.ObjectClasses.Rules.Section GetSection(Guid referenceId)
        {
            return GlobalResourceCache.Current.GetSection(referenceId);
        }

        private Glossary GetGlossary(Guid referenceId)
        {
            return GlobalResourceCache.Current.GetGlossary(referenceId);
        }

        private void GoToSection(IPSCCompanionLibrary.ObjectClasses.Rules.Section section)
        {
            Navigation.PushModalAsync(new SectionsPage(section, ""));
        }

        private void GoToRule(IPSCCompanionLibrary.ObjectClasses.Rules.Rule rule)
        {
            Navigation.PushModalAsync(new SectionsPage(rule));
        }

        private void GoToSubRule(IPSCCompanionLibrary.ObjectClasses.Rules.SubRule subRule)
        {
            var rule = GetRule(subRule.RuleId);

            rule.SpanList.Clear();
            rule.SubRules = new List<IPSCCompanionLibrary.ObjectClasses.Rules.SubRule>();
            rule.SubRules.Add(subRule);

            Navigation.PushModalAsync(new SectionsPage(rule));
        }

        private void ShowSubRuleReference(IPSCCompanionLibrary.ObjectClasses.Rules.SubRule subrule)
        {
            PopUpTitle.Text = $"{subrule.Numeric} {subrule.Name}";
            PopUpDescription.Text = subrule.Description;

            PopUpGrid.IsVisible = true;
        }

        private void ShowPdfReference(string pdfName, string friendlyName)
        {
            Navigation.PushModalAsync(new PDFPage(pdfName, friendlyName));
        }

        private void ShowRuleReference(IPSCCompanionLibrary.ObjectClasses.Rules.Rule rule)
        {
            PopUpTitle.Text = $"{rule.Numeric} {rule.Name}";
            PopUpDescription.Text = rule.Description;

            PopUpGrid.IsVisible = true;
        }

        private void ShowGlossaryTerm(Glossary glossary)
        {
            PopUpTitle.Text = glossary.Name;
            PopUpDescription.Text = glossary.Definition;

            PopUpGrid.IsVisible = true;
        }

        public class SubRule
        {
            public SubRule(IPSCCompanionLibrary.ObjectClasses.Rules.SubRule subRule)
            {
                Name = $"{subRule.Numeric} {subRule.Name}";
                Description = subRule.Description.Replace("\\r\\n", Environment.NewLine + Environment.NewLine);
                SpanList = subRule.SpanList;
            }

            public string Numeric { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public List<RuleSpan> SpanList { get; set; }
        }

        public class Rule : List<SubRule>
        {
            public Rule() { }

            public Rule(IPSCCompanionLibrary.ObjectClasses.Rules.Rule rule)
            {
                if (string.IsNullOrWhiteSpace(rule.Numeric))
                {
                    Name = rule.Name;
                }
                else
                {
                    Name = $"{rule.Numeric} {rule.Name}";
                }

                Description = rule.Description.Replace("\\r\\n", Environment.NewLine + Environment.NewLine);
                SpanList = rule.SpanList;

                if (rule.SubRules != null) 
                {
                    foreach (var subRule in rule.SubRules)
                    {
                        Add(new SubRule(subRule));
                    }
                }
            }

            public bool Visible => !string.IsNullOrWhiteSpace(Name);
            public string Name { get; set; }
            public string Description { get; set; }
            public List<RuleSpan> SpanList { get; set; }
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

        
    }
}